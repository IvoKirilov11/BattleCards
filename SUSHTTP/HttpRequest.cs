using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SUSHTTP
{
    public class HttpRequest
    {
        public static IDictionary<string, Dictionary<string, string>>
            Sessions = new Dictionary<string, Dictionary<string, string>>();
        public HttpRequest(string requestString)
        {
            Headers = new List<Header>();
            Cookies = new List<Cookie>();
            FormData = new Dictionary<string, string>();
            QueryData = new Dictionary<string, string>();

            var lines = requestString.Split(new string[] { HttpConstans.NewLine }, StringSplitOptions.None);
            var hederLine = lines[0];
            var hedarLineParts = hederLine.Split(' ');
            Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), hedarLineParts[0], true);
            Path = hedarLineParts[1];

            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex]; 
                lineIndex++;

                if(string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }
                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }

            }
            if(Headers.Any(x => x.Name == HttpConstans.RequestCookieHeader))
            {
                var cookiesAsString = Headers.FirstOrDefault(x => x.Name == HttpConstans.RequestCookieHeader).Value;
                var cookies = cookiesAsString.Split(new string[] {"; "},StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookieAsString in cookies)
                {
                    Cookies.Add(new Cookie(cookieAsString));
                }
            }
            
            var sessionCookie = Cookies.FirstOrDefault(x => x.Name == HttpConstans.SessionCookieName);
            if (sessionCookie == null)
            {
                var sessionId = Guid.NewGuid().ToString();
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionId, this.Session);
                this.Cookies.Add(new Cookie(HttpConstans.SessionCookieName, sessionId));
            }
            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionCookie.Value, this.Session);
            }
            else
            {
                this.Session = Sessions[sessionCookie.Value];
            }
            if(Path.Contains("?"))
            {
                var pathParts = this.Path.Split(new char[] { '?' }, 2);
                this.Path = pathParts[0];
                this.QueryString = pathParts[1];
            }
            else
            {
                this.QueryString = string.Empty;
            }

            Body = bodyBuilder.ToString().TrimEnd('\n','\r');

            SplitParameters(this.Body, this.FormData);
            SplitParameters(this.QueryString, this.QueryData);
        }
        private static void SplitParameters(string parametersAsString, IDictionary<string, string> output)
        {

            var parameters = parametersAsString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                var parameterParts = parameter.Split(new[] { '=' }, 2);
                var name = parameterParts[0];
                var value = WebUtility.UrlDecode(parameterParts[1]);
                if (!output.ContainsKey(name))
                {
                    output.Add(name, value);
                }
            }
        }

        public string Path { get; set; }

        public string QueryString { get; set; }

        public HttpMethod Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public IDictionary<string,string> FormData { get; set; }

        public IDictionary<string, string> QueryData { get; set; }

        public string Body { get; set; }

        public Dictionary<string, string> Session { get; set; }
    }
}

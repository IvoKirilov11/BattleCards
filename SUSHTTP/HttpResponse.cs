using System;
using System.Collections.Generic;
using System.Text;

namespace SUSHTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
        }
        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.Ok)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.StatusCode = statusCode;
            this.Body = body;
            this.Headers = new List<Header>
            {
                { new Header("Content-Type", contentType) },
                { new Header("Content-Length", body.Length.ToString()) },
            };
            this.Cookies = new List<Cookie>();
        }
        public override string ToString()
        {
            StringBuilder responseBuilder = new StringBuilder();
            responseBuilder.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}" + HttpConstans.NewLine);
            foreach (var header in this.Headers)
            {
                responseBuilder.Append(header.ToString() + HttpConstans.NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                responseBuilder.Append("Set-Cookie: " + cookie.ToString() + HttpConstans.NewLine);
            }

            responseBuilder.Append(HttpConstans.NewLine);

            return responseBuilder.ToString();
        }

        public HttpStatusCode StatusCode { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public byte[] Body { get; set; }
    }
}

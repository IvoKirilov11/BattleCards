namespace SUSHTTP
{
    public class Cookie
    {
        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }


        public Cookie(string cookieAsString)
        {
            var cookieParts = cookieAsString.Split(new char[] { '=' }, 2);
            Name = cookieParts[0];
            Value = cookieParts[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}
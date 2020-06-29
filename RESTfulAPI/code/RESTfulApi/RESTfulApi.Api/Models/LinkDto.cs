namespace RESTfulApi.Api.Models
{
    public class LinkDto
    {
        public LinkDto(string Href, string Rel, string Method)
        {
            this.Href = Href;
            this.Rel = Rel;
            this.Method = Method;
        }

        public string Href { get; }
        public string Rel { get; }
        public string Method { get; }
    }
}

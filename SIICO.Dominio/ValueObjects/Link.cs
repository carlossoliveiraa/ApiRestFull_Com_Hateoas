namespace SIICO.Dominio.ValueObjects
{
    public class Link
    {
        public string Href { get; set; } = string.Empty;
        public string Rel { get; set; } = string.Empty;
        public string Method { get; set; } = "GET";
        public string? Title { get; set; }

        public Link() { }

        public Link(string href, string rel, string method = "GET", string? title = null)
        {
            Href = href;
            Rel = rel;
            Method = method;
            Title = title;
        }

        public static Link Self(string href, string? title = null) => new(href, "self", "GET", title);
        public static Link Next(string href, string? title = null) => new(href, "next", "GET", title);
        public static Link Previous(string href, string? title = null) => new(href, "prev", "GET", title);
        public static Link First(string href, string? title = null) => new(href, "first", "GET", title);
        public static Link Last(string href, string? title = null) => new(href, "last", "GET", title);
    }
}

namespace Michaelsoft.ContentManager.Common.HttpModels.Content
{
    public class CreateRequest
    {

        public string Type { get; set; }

        public string Locale { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string TextContent { get; set; }

        public string HtmlContent { get; set; }

    }
}
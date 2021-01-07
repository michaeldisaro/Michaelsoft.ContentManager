using System.Collections.Generic;

namespace Michaelsoft.ContentManager.Common.HttpModels.Content
{
    public class Content
    {

        public string Id { get; set; }

        public string UrlFriendlyTitle { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string TextContent { get; set; }

        public string HtmlContent { get; set; }

        public List<string> Tags { get; set; }

    }
}
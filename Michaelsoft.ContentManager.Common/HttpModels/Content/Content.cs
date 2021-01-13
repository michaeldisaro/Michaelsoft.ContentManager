using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Michaelsoft.ContentManager.Common.HttpModels.Content
{
    public class Content
    {

        public string Id { get; set; }
        
        public string Type { get; set; }

        public string Locale { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime Published { get; set; }

        public string UrlFriendlyTitle { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string TextContent { get; set; }

        public string HtmlContent { get; set; }

        public List<string> Tags { get; set; }

    }
}
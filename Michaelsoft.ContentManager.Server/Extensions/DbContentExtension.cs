using Michaelsoft.ContentManager.Common.HttpModels.Content;
using Michaelsoft.ContentManager.Server.DatabaseModels;

namespace Michaelsoft.ContentManager.Server.Extensions
{
    public static class DbContentExtension
    {

        public static Content MapToContent(this DbContent dbContent,
                                           bool publicRequest = false)
        {
            return new Content
            {
                Id = publicRequest ? null : dbContent.Id,
                UrlFriendlyTitle = dbContent.UrlFriendlyTitle,
                Title = dbContent.Title,
                Subtitle = dbContent.Subtitle,
                TextContent = dbContent.TextContent,
                HtmlContent = dbContent.HtmlContent,
                Tags = dbContent.Tags
            };
        }

    }
}
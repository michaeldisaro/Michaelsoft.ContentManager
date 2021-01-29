using System.Collections.Generic;
using System.Linq;
using Michaelsoft.ContentManager.Client.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace Michaelsoft.ContentManager.Client.Enums
{
    public class ContentType
    {

        private static readonly Dictionary<string, string> ContentTypes = new Dictionary<string, string>
        {
            {"post", "Blog Post"},
            {"article", "Article"}
        };

        public static Dictionary<string, string> GetTypes()
        {
            return ContentTypes;
        }

        public static List<SelectListItem> GetTypesSelectListItems()
        {
            return ContentTypes.Select(kvp => new SelectListItem(kvp.Value, kvp.Key)).ToList();
        }

        public static bool IsValid(string contentType)
        {
            return ContentTypes.Keys.Contains(contentType);
        }

    }
}
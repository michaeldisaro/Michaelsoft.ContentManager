using System.Collections.Generic;
using System.Linq;
using Michaelsoft.ContentManager.Client.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Michaelsoft.ContentManager.Client.Enums
{
    public static class Locale
    {

        private static readonly Dictionary<string, string> Locales = new Dictionary<string, string>
        {
            {"it-IT", "Italy's Italian"},
            {"en-US", "US's English"}
        };

        public static Dictionary<string, string> GetLocales()
        {
            return Locales;
        }

        public static List<SelectListItem> GetLocalesSelectListItems()
        {
            return Locales.Select(kvp => new SelectListItem(kvp.Value, kvp.Key)).ToList();
        }

        public static bool IsValid(string locale)
        {
            return Locales.Keys.Contains(locale);
        }

    }
}
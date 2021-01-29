using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.ContentManager.Common.HttpModels.Authentication
{
    public class TokenRequest
    {

        [Required]
        [JsonRequired]
        [Display(Name = "author")]
        public string Author { get; set; }

    }
}
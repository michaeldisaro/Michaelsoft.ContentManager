using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.ContentManager.Common.HttpModels.Authentication
{
    public class AuthorizeRequest
    {

        [Required]
        [JsonRequired]
        public string EncryptedToken { get; set; }

    }
}
using Michaelsoft.ContentManager.Common.HttpModels.Authentication;

namespace Michaelsoft.ContentManager.Client.Models.Forms
{
    public class AuthorizeForm
    {

        public string Token { get; set; }

        public AuthorizeRequest AuthorizeRequest { get; set; }

        public string AuthorizeLabel { get; set; } = "authorize_label";

    }
}
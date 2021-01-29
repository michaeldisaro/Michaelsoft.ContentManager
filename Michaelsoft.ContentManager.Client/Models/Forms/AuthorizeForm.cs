using Michaelsoft.ContentManager.Common.HttpModels.Authentication;

namespace Michaelsoft.ContentManager.Client.Models.Forms
{
    public class AuthorizeForm
    {

        public string Token { get; set; }

        public AuthorizeRequest AuthorizeRequest { get; set; }

        public string AuthorizeLabel { get; set; } = "authorize_label";

        public string SuccessArea { get; set; } = "Content";

        public string SuccessPage { get; set; } = "/List";

        public string FailureArea { get; set; } = "Authentication";

        public string FailurePage { get; set; } = "/RequestToken";

    }
}
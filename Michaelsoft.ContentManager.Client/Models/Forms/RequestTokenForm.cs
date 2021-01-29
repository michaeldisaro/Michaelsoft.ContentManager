using Michaelsoft.ContentManager.Common.HttpModels.Authentication;

namespace Michaelsoft.ContentManager.Client.Models.Forms
{
    public class RequestTokenForm
    {

        public TokenRequest TokenRequest { get; set; }

        public string RequestTokenLabel { get; set; } = "request_token_label";

        public string SuccessArea { get; set; } = "Authentication";

        public string SuccessPage { get; set; } = "/Authorize";

        public string FailureArea { get; set; } = "Authentication";

        public string FailurePage { get; set; } = "/RequestToken";

    }
}
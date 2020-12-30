using Michaelsoft.ContentManager.Common.HttpModels.Authentication;

namespace Michaelsoft.ContentManager.Client.Models.Forms
{
    public class RequestTokenForm
    {

        public TokenRequest TokenRequest { get; set; }

        public string RequestTokenLabel { get; set; } = "request_token_label";

    }
}
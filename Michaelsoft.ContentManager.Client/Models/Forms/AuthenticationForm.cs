using Michaelsoft.ContentManager.Common.HttpModels.Authentication;

namespace Michaelsoft.ContentManager.Client.Models.Forms
{
    public class AuthenticationForm
    {
        
        public string LoginArea { get; set; } = "Authentication";

        public string LoginPage { get; set; } = "/RequestToken";

        public string LoginLabel { get; set; } = "login";

        public string LogoutSuccessArea { get; set; } = "Authentication";

        public string LogoutSuccessPage { get; set; } = "/RequestToken";

        public string LogoutFailureArea { get; set; } = "Content";

        public string LogoutFailurePage { get; set; } = "/List";

        public string LogoutLabel { get; set; } = "logout";

        public string UserClaim { get; set; } = "sub";

        public string UserMessage { get; set; } = "welcome_user";

    }
}
using Michaelsoft.ContentManager.Common.HttpModels.Content;

namespace Michaelsoft.ContentManager.Client.Models.Forms
{
    public class ContentForm
    {

        public Content Content { get; set; }

        public string ActionArea { get; set; }

        public string ActionPage { get; set; }

        public string SuccessArea { get; set; } = "Content";

        public string SuccessPage { get; set; } = "/List";

        public string FailureArea { get; set; } = "Content";

        public string FailurePage { get; set; } = "/List";

        public string SubmitLabel { get; set; }

    }
}
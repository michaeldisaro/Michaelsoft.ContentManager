using Michaelsoft.ContentManager.Common.HttpModels.Content;

namespace Michaelsoft.ContentManager.Client.Models.Forms
{
    public class ContentForm
    {
        public string Type { get; set; }

        public string Locale { get; set; }

        public Content Content { get; set; }

        public string Area { get; set; }

        public string Page { get; set; }

        public string SuccessArea { get; set; } = "Content";

        public string SuccessPage { get; set; } = "/List";

        public string FailureArea { get; set; } = "Content";

        public string FailurePage { get; set; } = "/List";

        public string SubmitLabel { get; set; }

    }
}
namespace Michaelsoft.ContentManager.Client.Models.Lists
{
    public class ContentList
    {

        public string ViewArea { get; set; } = "Content";

        public string ViewPage { get; set; } = "/View";

        public string ViewLabel { get; set; } = "link_detail";

        public string UpdateArea { get; set; } = "Content";

        public string UpdatePage { get; set; } = "/Update";

        public string UpdateLabel { get; set; } = "link_update";

        public string SuccessArea { get; set; } = "Content";

        public string SuccessPage { get; set; } = "/List";

        public string FailureArea { get; set; } = "Content";

        public string FailurePage { get; set; } = "/List";

    }
}
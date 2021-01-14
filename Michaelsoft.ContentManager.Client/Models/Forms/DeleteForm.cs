namespace Michaelsoft.ContentManager.Client.Models.Forms
{
    public class DeleteForm
    {

        public string Id { get; set; }

        public string SuccessArea { get; set; } = "Content";

        public string SuccessPage { get; set; } = "/List";

        public string FailureArea { get; set; } = "Content";

        public string FailurePage { get; set; } = "/Delete";

        public string SubmitLabel { get; set; } = "submit_delete";

    }
}
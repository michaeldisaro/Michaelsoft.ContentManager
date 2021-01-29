namespace Michaelsoft.ContentManager.Client.Models.Partials
{
    public class PagerModel
    {

        public string BaseUrl { get; set; }

        public int ActivePage { get; set; }

        public int ItemsPerPage { get; set; }
        
        public long TotalItems { get; set; }

        public int NumberOfPagerPages { get; set; } = 5;

        public string PageQueryParam { get; set; } = "page";

        public string ItemsQueryParam { get; set; } = "items";

    }
}
using System.Collections.Generic;

namespace Michaelsoft.ContentManager.Common.HttpModels.Content
{
    public class ContentListResponse : BaseResponse
    {

        public long TotalContents { get; set; }

        public List<Content> Contents { get; set; }

    }
}
using System.Collections.Generic;

namespace Michaelsoft.ContentManager.Common.HttpModels.Content
{
    public class ListResponse : BaseResponse
    {

        public List<Content> Contents { get; set; }

    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Common.HttpModels.Authentication;
using Michaelsoft.ContentManager.Common.HttpModels.Content;

namespace Michaelsoft.ContentManager.Client.Interfaces
{
    public interface IContentManagerContentApiService
    {

        public Task<ContentCreateResponse> Create(ContentCreateRequest contentCreateRequest);

        public Task<ContentUpdateResponse> Update(ContentUpdateRequest contentUpdateRequest);

        public Task<ContentListResponse> List(int page,
                                        int items);

        public Task<Content> Read(string id);

        public Task<List<Content>> PublicList();

        public Task<Content> PublicRead(string urlFriendlyTitle);

        public Task<ContentDeleteResponse> Delete(string id);

    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Common.HttpModels.Authentication;
using Michaelsoft.ContentManager.Common.HttpModels.Content;

namespace Michaelsoft.ContentManager.Client.Interfaces
{
    public interface IContentManagerContentApiService
    {

        public Task<CreateResponse> CreateContent(CreateRequest createRequest);

        public Task<UpdateResponse> UpdateContent(UpdateRequest updateRequest);

        public Task<List<Content>> ListContents();

        public Task<Content> ReadContent(string contentId);

        public Task<List<Content>> PublicListContents();

        public Task<Content> PublicReadContent(string urlFriendlyTitle);

    }
}
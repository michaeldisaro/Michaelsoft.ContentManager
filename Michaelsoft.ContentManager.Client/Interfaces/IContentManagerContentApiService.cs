using System.Threading.Tasks;
using Michaelsoft.ContentManager.Common.HttpModels.Authentication;
using Michaelsoft.ContentManager.Common.HttpModels.Content;

namespace Michaelsoft.ContentManager.Client.Interfaces
{
    public interface IContentManagerContentApiService
    {

        public Task<CreateResponse> CreateContent(CreateRequest createRequest);

        public Task<ListResponse> ListContents();

    }
}
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Common.HttpModels.Media;

namespace Michaelsoft.ContentManager.Client.Interfaces
{
    public interface IContentManagerMediaApiService
    {

        public Task<MediaCreateResponse> Create(MediaCreateRequest mediaCreateRequest);
        
        public Task<byte[]> Read(string filenameWithExtension);

    }
}
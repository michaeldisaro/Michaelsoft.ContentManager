using System.Threading.Tasks;
using Michaelsoft.ContentManager.Common.HttpModels.Authentication;

namespace Michaelsoft.ContentManager.Client.Interfaces
{
    public interface IContentManagerAuthenticationApiService
    {

        public Task<TokenResponse> RequestToken(string author);

        public Task<AuthorizeResponse> Authorize(string encodedToken);

        public Task<LogoutResponse> Logout();

    }
}
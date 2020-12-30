using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using Michaelsoft.ContentManager.Common.Encryption;
using Michaelsoft.ContentManager.Common.Extensions;
using Michaelsoft.ContentManager.Common.HttpModels.Authentication;
using Michaelsoft.ContentManager.Server.Services;
using Michaelsoft.ContentManager.Server.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.ContentManager.Server.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthenticationController : Controller
    {

        private const string AccessTokenType = "access-token";

        private readonly TokenService _tokenService;

        private readonly IAsymmetricEncryptionSettings _asymmetricEncryptionSettings;

        public AuthenticationController(TokenService tokenService,
                                        IAsymmetricEncryptionSettings asymmetricEncryptionSettings)
        {
            _asymmetricEncryptionSettings = asymmetricEncryptionSettings;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public TokenResponse Token([FromBody]
                                   TokenRequest tokenRequest)
        {
            try
            {
                var value = (tokenRequest.Author + DateTime.Now + StringHelper.RandomString(4)).Sha1();
                _tokenService.Create(AccessTokenType, value, tokenRequest.Author, 90);
                return new TokenResponse
                {
                    Token = value
                };
            }
            catch (Exception ex)
            {
                return new TokenResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public AuthorizeResponse Authorize([FromBody]
                                           AuthorizeRequest authorizeRequest)
        {
            try
            {
                var value = RsaHelper
                    .DecryptString(authorizeRequest.EncryptedToken,
                                   _asymmetricEncryptionSettings.PublicKey,
                                   false, true);
                var token = _tokenService.GetTokenByTypeAndValue(AccessTokenType, value);

                var claims = new List<Claim>
                {
                    new Claim("sub", token.Author)
                };

                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                HttpContext.User = new ClaimsPrincipal(identity);

                _tokenService.Delete(token.Id);

                return new AuthorizeResponse();
            }
            catch (Exception ex)
            {
                return new AuthorizeResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public LogoutResponse Logout()
        {
            try
            {
                HttpContext.User = null;
                return new LogoutResponse();
            }
            catch (Exception ex)
            {
                return new LogoutResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}
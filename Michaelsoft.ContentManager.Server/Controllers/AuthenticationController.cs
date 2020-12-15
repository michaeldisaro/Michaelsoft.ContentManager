using System;
using System.Collections.Generic;
using System.Security.Claims;
using Michaelsoft.ContentManager.Common.Extensions;
using Michaelsoft.ContentManager.Server.Services;
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

        public AuthenticationController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpGet("[action]/{author}")]
        [Produces("application/json")]
        public string GetToken(string author)
        {
            try
            {
                var value = (author + StringHelper.RandomString(10)).Sha1();
                _tokenService.Create(AccessTokenType, value, author, 900);
                return value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public bool Authorize([FromBody]
                              string encodedToken)
        {
            try
            {
                var value = encodedToken; // TODO: Decode with public key
                var token = _tokenService.GetTokenByTypeAndValue(AccessTokenType, value);

                var claims = new List<Claim>
                {
                    new Claim("sub", token.Author)
                };

                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                HttpContext.User = new ClaimsPrincipal(identity);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public bool Logout()
        {
            try
            {
                HttpContext.User = null;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
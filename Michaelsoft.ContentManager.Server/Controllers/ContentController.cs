using System;
using System.Collections.Generic;
using Michaelsoft.ContentManager.Common.Extensions;
using Michaelsoft.ContentManager.Common.HttpModels.Content;
using Michaelsoft.ContentManager.Server.DatabaseModels;
using Michaelsoft.ContentManager.Server.Services;
using Michaelsoft.ContentManager.Server.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.ContentManager.Server.Controllers
{
    [ApiController]
    [Route("/")]
    public class ContentController : Controller
    {

        private readonly ContentService _contentService;

        public ContentController(ContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        public ListResponse List()
        {
            try
            {
                var contents = new List<Content>();

                var dbContents = _contentService.GetAll();
                foreach (var dbContent in dbContents)
                {
                    contents.Add(new Content
                    {
                        Id = dbContent.Id,
                        UrlFriendlyTitle = dbContent.UrlFriendlyTitle,
                        Title = dbContent.Title,
                        Subtitle = dbContent.Subtitle,
                        TextContent = dbContent.TextContent,
                        HtmlContent = dbContent.HtmlContent,
                        Tags = dbContent.Tags
                    });
                }

                return new ListResponse
                {
                    Contents = contents
                };
            }
            catch (Exception ex)
            {
                return new ListResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public CreateResponse Create([FromBody]
                                     CreateRequest createRequest)
        {
            try
            {
                var dbContent = new DbContent
                {
                    Type = createRequest.Type,
                    Locale = createRequest.Locale,
                    Author = HttpContextUtility.LoggedUserIdentity(),
                    Title = createRequest.Title,
                    Subtitle = createRequest.Subtitle,
                    HtmlContent = createRequest.HtmlContent,
                    TextContent = createRequest.TextContent
                };

                var newContent = _contentService.Create(dbContent);

                return new CreateResponse
                {
                    Id = newContent.Id
                };
            }
            catch (Exception ex)
            {
                return new CreateResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}
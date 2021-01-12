using System;
using System.Collections.Generic;
using Michaelsoft.ContentManager.Common.Extensions;
using Michaelsoft.ContentManager.Common.HttpModels.Content;
using Michaelsoft.ContentManager.Server.DatabaseModels;
using Michaelsoft.ContentManager.Server.Extensions;
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

        #region FOR ADMIN

        [Authorize]
        [HttpGet("[action]/{id}")]
        [Produces("application/json")]
        public ReadResponse Read(string id)
        {
            try
            {
                var dbContent = _contentService.Get(id);

                return new ReadResponse
                {
                    Content = dbContent.MapToContent()
                };
            }
            catch (Exception ex)
            {
                return new ReadResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        [Produces("application/json")]
        public ListResponse List()
        {
            try
            {
                var contents = new List<Content>();

                var dbContents = _contentService.GetAll();
                foreach (var dbContent in dbContents)
                    contents.Add(dbContent.MapToContent());

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
                    Title = createRequest.Content.Title,
                    Subtitle = createRequest.Content.Subtitle,
                    HtmlContent = createRequest.Content.HtmlContent,
                    TextContent = createRequest.Content.TextContent
                };

                var newContent = _contentService.Create(dbContent);

                return new CreateResponse
                {
                    Content = newContent.MapToContent()
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

        [Authorize]
        [HttpPut("[action]/{id}")]
        [Produces("application/json")]
        public UpdateResponse Update(string id,
                                     [FromBody]
                                     UpdateRequest updateRequest)
        {
            try
            {
                var dbContent = new DbContent
                {
                    Type = updateRequest.Type,
                    Locale = updateRequest.Locale,
                    Author = HttpContextUtility.LoggedUserIdentity(),
                    Title = updateRequest.Content.Title,
                    Subtitle = updateRequest.Content.Subtitle,
                    HtmlContent = updateRequest.Content.HtmlContent,
                    TextContent = updateRequest.Content.TextContent
                };

                var updatedContent = _contentService.Update(id, dbContent);

                return new UpdateResponse
                {
                    Content = updatedContent.MapToContent()
                };
            }
            catch (Exception ex)
            {
                return new UpdateResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        #endregion

        #region FOR GUESTS

        [HttpGet("[action]/{urlFriendlyTitle}")]
        [Produces("application/json")]
        public ReadResponse PublicRead(string urlFriendlyTitle)
        {
            try
            {
                var dbContent = _contentService.GetByUrlFriendlyTitle(urlFriendlyTitle);

                return new ReadResponse
                {
                    Content = dbContent.MapToContent(true)
                };
            }
            catch (Exception ex)
            {
                return new ReadResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        public ListResponse PublicList()
        {
            try
            {
                var contents = new List<Content>();

                var dbContents = _contentService.GetAll();
                foreach (var dbContent in dbContents)
                    contents.Add(dbContent.MapToContent(true));

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

        #endregion

    }
}
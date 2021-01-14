using System;
using System.Collections.Generic;
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
    [Route("/Content/")]
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
        public ContentReadResponse Read(string id)
        {
            try
            {
                var dbContent = _contentService.Get(id);

                return new ContentReadResponse
                {
                    Content = dbContent.MapToContent()
                };
            }
            catch (Exception ex)
            {
                return new ContentReadResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        [Produces("application/json")]
        public ContentListResponse List()
        {
            try
            {
                var contents = new List<Content>();

                var dbContents = _contentService.GetAll();
                foreach (var dbContent in dbContents)
                    contents.Add(dbContent.MapToContent());

                return new ContentListResponse
                {
                    Contents = contents
                };
            }
            catch (Exception ex)
            {
                return new ContentListResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public ContentCreateResponse Create([FromBody]
                                            ContentCreateRequest contentCreateRequest)
        {
            try
            {
                var dbContent = new DbContent
                {
                    Author = HttpContextUtility.LoggedUserIdentity(),
                    Type = contentCreateRequest.Content.Type,
                    Locale = contentCreateRequest.Content.Locale,
                    Published = contentCreateRequest.Content.Published,
                    Title = contentCreateRequest.Content.Title,
                    Subtitle = contentCreateRequest.Content.Subtitle,
                    HtmlContent = contentCreateRequest.Content.HtmlContent,
                    TextContent = contentCreateRequest.Content.TextContent
                };

                var newContent = _contentService.Create(dbContent);

                return new ContentCreateResponse
                {
                    Content = newContent.MapToContent()
                };
            }
            catch (Exception ex)
            {
                return new ContentCreateResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [Authorize]
        [HttpPut("[action]/{id}")]
        [Produces("application/json")]
        public ContentUpdateResponse Update(string id,
                                            [FromBody]
                                            ContentUpdateRequest contentUpdateRequest)
        {
            try
            {
                var dbContent = new DbContent
                {
                    Author = HttpContextUtility.LoggedUserIdentity(),
                    Type = contentUpdateRequest.Content.Type,
                    Locale = contentUpdateRequest.Content.Locale,
                    Published = contentUpdateRequest.Content.Published,
                    Title = contentUpdateRequest.Content.Title,
                    Subtitle = contentUpdateRequest.Content.Subtitle,
                    HtmlContent = contentUpdateRequest.Content.HtmlContent,
                    TextContent = contentUpdateRequest.Content.TextContent
                };

                var updatedContent = _contentService.Update(id, dbContent);

                return new ContentUpdateResponse
                {
                    Content = updatedContent.MapToContent()
                };
            }
            catch (Exception ex)
            {
                return new ContentUpdateResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [Authorize]
        [HttpDelete("[action]/{id}")]
        [Produces("application/json")]
        public ContentDeleteResponse Delete(string id)
        {
            try
            {
                _contentService.Delete(id);
                return new ContentDeleteResponse();
            }
            catch (Exception ex)
            {
                return new ContentDeleteResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        #endregion

        #region FOR GUESTS

        [HttpGet("Public/Read/{urlFriendlyTitle}")]
        [Produces("application/json")]
        public ContentReadResponse PublicRead(string urlFriendlyTitle)
        {
            try
            {
                var dbContent = _contentService.GetByUrlFriendlyTitle(urlFriendlyTitle);

                return new ContentReadResponse
                {
                    Content = dbContent.MapToContent(true)
                };
            }
            catch (Exception ex)
            {
                return new ContentReadResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpGet("Public/List")]
        [Produces("application/json")]
        public ContentListResponse PublicList()
        {
            try
            {
                var contents = new List<Content>();

                var dbContents = _contentService.GetAll();
                foreach (var dbContent in dbContents)
                    contents.Add(dbContent.MapToContent(true));

                return new ContentListResponse
                {
                    Contents = contents
                };
            }
            catch (Exception ex)
            {
                return new ContentListResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        #endregion

    }
}
using System;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Common.HttpModels.Media;
using Michaelsoft.ContentManager.Server.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.ContentManager.Server.Controllers
{
    [ApiController]
    [Route("Media/")]
    public class MediaController : Controller
    {

        [HttpPost("[action]")]
        [Produces("application/json")]
        public async Task<MediaCreateResponse> Create([FromBody]
                                                 MediaCreateRequest mediaCreateRequest)
        {
            try
            {
                var filenameWithExtension = $"{mediaCreateRequest.Filename}.{mediaCreateRequest.Extension}";
                await MediaStorageUtility.CreateMedia(mediaCreateRequest.Content, mediaCreateRequest.Extension, filenameWithExtension);
                return new MediaCreateResponse
                {
                    FilenameWithExtension = filenameWithExtension
                };
            }
            catch (Exception ex)
            {
                return new MediaCreateResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpGet("[action]/{filenameWithExtension}")]
        [Produces("application/json")]
        public async Task<MediaReadResponse> Read(string filenameWithExtension)
        {
            try
            {
                var extension = filenameWithExtension.Split(".")[^1];
                var content = await MediaStorageUtility.GetMedia(extension, filenameWithExtension);
                return new MediaReadResponse
                {
                    Content = content
                };
            }
            catch (Exception ex)
            {
                return new MediaReadResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Models.Controllers;
using Michaelsoft.ContentManager.Client.Utilities;
using Michaelsoft.ContentManager.Common.Extensions;
using Michaelsoft.ContentManager.Common.HttpModels.Media;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.ContentManager.Client.Controllers
{
    [ApiController]
    [Route("Media/")]
    public class MediaController : Controller
    {

        private readonly IContentManagerMediaApiService _mediaApiService;

        public MediaController(IContentManagerMediaApiService mediaApiService)
        {
            _mediaApiService = mediaApiService;
        }

        [HttpPost("[action]")]
        [Produces("application/json")]
        public async Task<MediaUploadResult> Upload()
        {
            try
            {
                var contentFile = Request.Form.Files.FirstOrDefault();

                if (contentFile == null || contentFile.Length <= 0)
                    throw new Exception("No file uploaded!");

                var extension = contentFile.FileName.Split(".")[^1].ToLower();

                if (contentFile.Length > 1024 * 1024)
                    throw new Exception("Media too large!");

                if (!new[] {"jpg", "jpeg", "png", "webp"}.Contains(extension))
                    throw new Exception("Media type not supported!");

                var ms = new MemoryStream();
                await contentFile.CopyToAsync(ms);
                var content = ms.ToArray();
                
                var filename = Encoding.UTF8.GetString(content).Sha1().ToLower();

                var result = await _mediaApiService.Create(new MediaCreateRequest
                {
                    Content = content,
                    Filename = filename,
                    Extension = extension
                });

                return new MediaUploadResult
                {
                    Url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Media/{result.FilenameWithExtension}"
                };
            }
            catch (Exception e)
            {
                return new MediaUploadResult
                {
                    Error = e.Message
                };
            }
        }
        
        [HttpGet("{filenameWithExtension}")]
        [Produces("application/json")]
        public async Task<FileContentResult> Read(string filenameWithExtension)
        {
            try
            {
                var image = await _mediaApiService.Read(filenameWithExtension);
                return image == null ? null : new FileContentResult(image, MediaUtility.GetMimeTypeFromMediaName(filenameWithExtension));
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
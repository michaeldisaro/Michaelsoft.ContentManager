using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Models.Controllers;
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
            var url = "";
            try
            {
                var contentFile = Request.Form.Files.FirstOrDefault();

                if (contentFile == null || contentFile.Length <= 0)
                    throw new Exception("No file uploaded!");

                var filename = $"{contentFile.FileName}-{StringHelper.RandomString(6)}".Sha1().ToLower();
                var extension = contentFile.FileName.Split(".")[^1].ToLower();

                if (contentFile.Length > 1024 * 1024)
                    throw new Exception("Media too large!");

                if (!new[] {"jpg", "jpeg", "png", "webp"}.Contains(extension))
                    throw new Exception("Media type not supported!");

                var ms = new MemoryStream();
                await contentFile.CopyToAsync(ms);
                var content = ms.ToArray();

                var result = await _mediaApiService.Create(new MediaCreateRequest
                {
                    Content = content,
                    Filename = filename,
                    Extension = extension
                });

                url = $"{HttpContext.Request.Host}/Media/{result.FilenameWithExtension}";

                return new MediaUploadResult
                {
                    Url = url
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

    }
}
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Models.Forms;
using Michaelsoft.ContentManager.Common.HttpModels.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.ContentManager.Client.Areas.Content.Pages
{
    public class Create : PageModel
    {

        private readonly IContentManagerContentApiService _contentApiService;

        public Create(IContentManagerContentApiService contentApiService)
        {
            _contentApiService = contentApiService;
        }

        [BindProperty]
        public ContentForm ContentForm { get; set; }

        public void OnGet()
        {
            ContentForm = new ContentForm
            {
                Locale = "it-IT",
                Type = "post",
                Area = "Content",
                Page = "Create",
                Content = new Common.HttpModels.Content.Content(),
                SubmitLabel = "create_label"
            };
        }

        public async Task<IActionResult> OnPost()
        {
            var createRequest = new CreateRequest
            {
                Type = ContentForm.Type,
                Locale = ContentForm.Locale,
                Content = ContentForm.Content
            };

            var result = await _contentApiService.CreateContent(createRequest);

            if (result.Success)
                return RedirectToPage(ContentForm.SuccessPage, new {Area = ContentForm.SuccessArea});

            return RedirectToPage(ContentForm.FailurePage, new {Area = ContentForm.FailureArea});
        }

    }
}
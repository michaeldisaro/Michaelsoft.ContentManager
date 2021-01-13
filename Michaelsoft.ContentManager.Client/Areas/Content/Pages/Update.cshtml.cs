using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Models.Forms;
using Michaelsoft.ContentManager.Common.HttpModels.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.ContentManager.Client.Areas.Content.Pages
{
    public class Update : PageModel
    {

        private readonly IContentManagerContentApiService _contentApiService;

        public Update(IContentManagerContentApiService contentApiService)
        {
            _contentApiService = contentApiService;

        }

        [BindProperty]
        public ContentForm ContentForm { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            var content = await _contentApiService.ReadContent(id);
            ContentForm = new ContentForm
            {
                ActionArea = "Content",
                ActionPage = "/Update",
                Content = content,
                SubmitLabel = "label_update"
            };
            return Page();
        }

        public async Task<IActionResult> OnPost(string id)
        {
            var updateRequest = new UpdateRequest
            {
                Content = ContentForm.Content
            };

            var result = await _contentApiService.UpdateContent(updateRequest);

            if (result.Success)
                return RedirectToPage(ContentForm.SuccessPage, new {Area = ContentForm.SuccessArea});

            return RedirectToPage(ContentForm.FailurePage, new {Area = ContentForm.FailureArea});
        }

    }
}
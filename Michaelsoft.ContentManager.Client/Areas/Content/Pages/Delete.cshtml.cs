using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.ContentManager.Client.Areas.Content.Pages
{
    public class Delete : PageModel
    {

        private readonly IContentManagerContentApiService _contentApiService;

        public Delete(IContentManagerContentApiService contentApiService)
        {
            _contentApiService = contentApiService;
        }

        [BindProperty]
        public DeleteForm DeleteForm { get; set; }

        public void OnGet(string id)
        {
            DeleteForm = new DeleteForm
            {
                Id = id
            };
        }

        public async Task<IActionResult> OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage(DeleteForm.FailurePage,
                                      new {Area = DeleteForm.FailureArea, Id = DeleteForm.Id});
            }

            var response = await _contentApiService.Delete(id);

            return response.Success
                       ? RedirectToPage(DeleteForm.SuccessPage, 
                                        new {Area = DeleteForm.SuccessArea})
                       : RedirectToPage(DeleteForm.FailurePage,
                                        new {Area = DeleteForm.FailureArea, Id = DeleteForm.Id});

        }

    }
}
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Models.Forms;
using Michaelsoft.ContentManager.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.ContentManager.Client.Areas.Authentication.Pages
{
    public class Authorize : PageModel
    {

        private readonly IContentManagerAuthenticationApiService _authenticationApiService;

        public Authorize(IContentManagerAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public AuthorizeForm AuthorizeForm { get; set; }

        public IActionResult OnGet(string token)
        {
            if (token == null)
                return NotFound();

            AuthorizeForm = new AuthorizeForm
            {
                Token = token
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(string token)
        {
            if (!AuthorizeForm.AuthorizeRequest.EncryptedToken.IsNullOrEmpty())
            {
                var authorizeResponse =
                    await _authenticationApiService.Authorize(AuthorizeForm.AuthorizeRequest.EncryptedToken);
                if (authorizeResponse.Success)
                    return RedirectToPage("Create", new {Area = "Content"});
            }

            return Page();
        }

    }
}
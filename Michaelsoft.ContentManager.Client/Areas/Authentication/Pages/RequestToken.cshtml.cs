using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Models.Forms;
using Michaelsoft.ContentManager.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.ContentManager.Client.Areas.Authentication.Pages
{
    public class RequestToken : PageModel
    {

        private readonly IContentManagerAuthenticationApiService _authenticationApiService;

        public RequestToken(IContentManagerAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public RequestTokenForm RequestTokenForm { get; set; }

        public void OnGet()
        {
            RequestTokenForm = new RequestTokenForm();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!RequestTokenForm.TokenRequest.Author.IsNullOrEmpty())
            {
                var tokenResponse = await _authenticationApiService.RequestToken(RequestTokenForm.TokenRequest.Author);
                if (tokenResponse.Success)
                    return RedirectToPage("/Authorize", new {Area = "Authentication", token = tokenResponse.Token});
            }

            return Page();
        }

    }
}
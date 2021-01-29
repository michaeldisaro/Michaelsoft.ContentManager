using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.ContentManager.Client.Areas.Authentication.Pages
{
    public class Logout : PageModel
    {

        private readonly IContentManagerAuthenticationApiService _authenticationApiService;

        public Logout(IContentManagerAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public AuthenticationForm AuthenticationForm { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var response = await _authenticationApiService.Logout();

            if (response.Success)
            {
                TempData["Message"] = "Logout succeed!";
                return RedirectToPage(AuthenticationForm.LogoutSuccessPage,
                                      new {Area = AuthenticationForm.LogoutSuccessArea});
            }

            TempData["Message"] = "Logout failed.";
            return RedirectToPage(AuthenticationForm.LogoutFailurePage,
                                  new {Area = AuthenticationForm.LogoutFailureArea});
        }

    }
}
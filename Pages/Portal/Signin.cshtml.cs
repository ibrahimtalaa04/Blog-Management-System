using BlogManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogManagementSystem.Pages.Portal
{
    public class SigninModel : PageModel
    {
        public SigninModel()
        {
            
        }

        [BindProperty]
        public SigninViewModel signinViewModel { get; set; }

        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            UserModel? userModel = UserRepository.GetByEmail(signinViewModel.Email);

            if (userModel== null)
            {
                ModelState.AddModelError("signinViewModel.Email", "Email is not exist!");
                return Page();
            }
            else 
            {
                if(userModel.password!=signinViewModel.Password)
                {
                    ModelState.AddModelError("signinViewModel.Password", "Wrong Password !");
                    return Page();
                }

                UserRepository.User = userModel;
                return RedirectToPage("/index");
            }

        }
    }
}

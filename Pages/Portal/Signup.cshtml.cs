namespace BlogManagementSystem.Pages.Portal
{
    public class SignupModel : PageModel
    {
        public SignupModel()
        {
            
        }


        [BindProperty]
        public SignupViewModel signupViewModel { get; set; }






        public void OnGet()
        {



        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if(UserRepository.GetByEmail(signupViewModel.Email)!=null)
            {
                ModelState.AddModelError("signupViewModel.Email", "Email is exist! write another");
                return Page();
            }

            signupViewModel.imagePath = "userdefault.jpg";
            UserRepository.Signup(signupViewModel);

            return RedirectToPage("/Index");
        }





        


    }
}

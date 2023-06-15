namespace BlogManagementSystem.ViewModels
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Your first name is required")]
        [StringLength(30, ErrorMessage = "Your first name length must be between 5 and 30.", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }


        [Required(ErrorMessage = "Your last name is required")]
        [StringLength(30, ErrorMessage = "Your last name length must be between 3 and 30.", MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Your Email is required !")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Your Password is required !")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Your Confirm Password  is required !")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }


        public bool IAgree { get; set; }
    }
}

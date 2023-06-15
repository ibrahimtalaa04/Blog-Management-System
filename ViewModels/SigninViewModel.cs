namespace BlogManagementSystem.ViewModels
{
    public class SigninViewModel
    {
        [Required(ErrorMessage = "Your Email is required !")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Your Password is required !")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Enter at least {2} characters", MinimumLength = 6)]
        public string? Password { get; set; }
    }
}

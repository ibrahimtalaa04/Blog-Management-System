using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50,ErrorMessage ="First Name must be less than 50 char !")]
        [Required(ErrorMessage ="First Name is required !")]
        public string? firstName { get; set; }

        [StringLength(50,ErrorMessage ="Last Name must be less than 50 char!")]
        [Required(ErrorMessage ="Last name is required !")]
        public string? lastName { get; set; }

        [StringLength(80,ErrorMessage ="Email must be less than 50 char!")]
        [Required(ErrorMessage ="Email is required !")]
        public string? email { get; set; }

        [Required(ErrorMessage ="Password is required !")]
        [StringLength(200)]
        public string? password { get; set; }

        [StringLength(200)]
        public string? imagePath { get; set; }

        public DateTime dateJoined { get; set; }=DateTime.Now;

    }
}

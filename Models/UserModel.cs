using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? firstName { get; set; }

        [StringLength(50)]
        public string? lastName { get; set; }

        [StringLength(80)]
        public string? email { get; set; }
        
        public string? password { get; set; }

        [StringLength(200)]
        public string? imagePath { get; set; }
        public DateTime dateJoined { get; set; }=DateTime.Now;

    }
}

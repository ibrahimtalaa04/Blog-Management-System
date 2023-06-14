namespace BlogManagementSystem.Models
{
    public class BlogModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string? title { get; set; }


        [DataType(DataType.MultilineText)]
        [Required]
        public string? content { get; set; }

        public DateTime? dateAdded { get; set; }= DateTime.Now;

        [StringLength(200)]

        public string? imagePath { get; set; }

        [Required]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel? UserBlog { get; set; }

    }
}

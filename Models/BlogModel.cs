namespace BlogManagementSystem.Models
{
    public class BlogModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100,ErrorMessage ="Title must be less than 100 char!")]
        [Required(ErrorMessage ="Title is required!")]
        public string? title { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Content is required!")]
        public string? content { get; set; }

        public DateTime? dateAdded { get; set; }= DateTime.Now;

        [StringLength(200)]
        public string? imagePath { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel? UserBlog { get; set; }

    }
}

namespace BlogManagementSystem.Models
{
    public class BlogModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100,ErrorMessage ="Title must be less than 100 char!")]
        [Required(ErrorMessage ="Title is required!")]
        [Display(Name ="Title")]
        public string? title { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Content is required!")]
        [Display(Name = "Content")]
        public string? content { get; set; }
        
        [Required(ErrorMessage ="Category is required!")]
        [Display(Name = "Category")]
        public string? Category { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? dateAdded { get; set; }= DateTime.Now;


        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        [StringLength(200)]
        [Display(Name = "Image Name")]
        public string? imagePath { get; set; } = "defaultblog.jpg";

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel? UserBlog { get; set; }

    }
}

namespace BlogManagementSystem.Models
{
    public class RoleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Role Name is required !")]
        [StringLength(100,ErrorMessage ="Role name must be less than 100")]
        public string? Role { get; set; }

    }
}

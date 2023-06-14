using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogManagementSystem.Pages.Blogs
{
    public class AddModel : PageModel
    {



        [BindProperty]
        public BlogModel blogModel { get; set; }

        public void OnGet()
        {
        }
    }
}

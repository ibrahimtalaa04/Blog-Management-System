using BlogManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogManagementSystem.Pages.Blogs
{
    public class IndexModel : PageModel
    {


        public List<BlogModel> Blogs { get; set; }
        public IndexModel()
        {
            Blogs=BlogList.GetAll().ToList();
        }


        public void OnGet()
        {
        }


        public IActionResult OnPost(int Id)
        {
            BlogModel? blog=BlogList.Get(Id);
            if (blog == null) 
            {
                return NotFound();
            }

            BlogList.Delete(Id);
            return RedirectToPage();
        }


    }
}

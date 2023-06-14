using BlogManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogManagementSystem.Pages.Blogs
{
    public class IndexModel : PageModel
    {


        public BindingList<BlogModel> Blogs { get; set; }
        public IndexModel()
        {
            Blogs=BlogList.GetAll();
        }


        public void OnGet()
        {
        }


        public void OnPost()
        {

        }


    }
}

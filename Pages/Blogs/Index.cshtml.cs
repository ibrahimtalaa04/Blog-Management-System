using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace BlogManagementSystem.Pages.Blogs
{
    public class IndexModel : PageModel
    {


        public IndexModel()
        {
        }


        public List<BlogModel> Blogs { get; set; }= new List<BlogModel>();

        public IEnumerable<SelectListItem> lstPageSize { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem(){Value="5",Text="5"},
            new SelectListItem(){Value="10",Text="10"},
            new SelectListItem(){Value="20",Text="20"},
        };

        public IEnumerable<SelectListItem> lstSort { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem(){Value="Id",Text="Id"},
            new SelectListItem(){Value="Title",Text="Title"},
        };


        public QueryPageResult pageResult { get; set; } = new QueryPageResult();


        [BindProperty(SupportsGet = true)]
        public BlogQueryParamters queryParamters { get; set; }=new BlogQueryParamters();


        public void OnGet()
        {
            Blogs = BlogList.GetAll();



            if (!string.IsNullOrEmpty(queryParamters.SearchTearm))
            {
                Blogs = Blogs.Where(
                 p => p.title!.ToLower().Contains(queryParamters.SearchTearm.ToLower()) || p.content!.ToLower().Contains(queryParamters.SearchTearm.ToLower())).ToList();
            }


            pageResult.TotalCount = Blogs.Count();
            pageResult.TotalPages = (int)Math.Ceiling(pageResult.TotalCount / (double)queryParamters.Size);

            if ((queryParamters.CurPage - 1) > 0)
                pageResult.PreviousPage = queryParamters.CurPage - 1;

            if ((queryParamters.CurPage + 1) <= pageResult.TotalPages)
                pageResult.NextPage = queryParamters.CurPage + 1;

            if (pageResult.TotalPages == 0)
                pageResult.FirstRowOnPage = pageResult.LastRowOnPage = 0;
            else
            {
                pageResult.FirstRowOnPage = (queryParamters.CurPage - 1) * queryParamters.Size + 1;
                pageResult.LastRowOnPage = Math.Min(queryParamters.CurPage * queryParamters.Size, pageResult.TotalCount);
            }

            Blogs = Blogs.Skip(queryParamters.Size * (queryParamters.CurPage - 1))
              .Take(queryParamters.Size).ToList();

        }


        public IActionResult OnPost([FromForm]int Id)
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

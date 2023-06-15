using BlogManagementSystem.Data;
namespace BlogManagementSystem.Pages.Blogs
{
    public class AddModel : PageModel
    {
        readonly IWebHostEnvironment _iwh;

        public AddModel(IWebHostEnvironment iwh)
        {
            _iwh = iwh;
        }


        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }

        [BindProperty]
        public BlogModel blogModel { get; set; }

        public IActionResult OnGet()
        {

            if (Id != 0)
            {
                blogModel = BlogList.Get(Id);
                if(blogModel == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            blogModel.UserId = 1;
            blogModel.dateAdded= DateTime.Now;

            if(blogModel.ImageFile !=null)
            {
                blogModel.imagePath = await UploadImage(blogModel.ImageFile, blogModel.imagePath);
            }
            else 
                blogModel.imagePath = "defaultblog.jpg";

            if(blogModel.Id == 0)
            {
                BlogList.Insert(blogModel);

            }
            else
            {
                BlogList.Update(blogModel);
            }
            
            return RedirectToPage("Index");
        }

        public async Task<string> UploadImage(IFormFile image, string? oldFileName)
        {
            if (oldFileName is not null)
            {
                DeleteImage(oldFileName);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + image.FileName;

            string rootpath = Path.Combine(_iwh.WebRootPath, "img");
            if (!Directory.Exists(rootpath))
                Directory.CreateDirectory(rootpath);

            string Filepath = Path.Combine(rootpath, fileName);
            FileStream fileStream = new FileStream(Filepath, FileMode.Create);
            await image.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            fileStream.Close();
            return fileName;
        }

        private void DeleteImage(string oldFileName)
        {
            string Filepath = Path.Combine(_iwh.WebRootPath, "img", oldFileName);
            FileInfo fi = new FileInfo(Filepath);
            if (System.IO.File.Exists(fi.ToString()))
            {
                System.IO.File.Delete(Filepath);
            }
        }
    }
}

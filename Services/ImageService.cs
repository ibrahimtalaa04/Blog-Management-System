namespace BlogManagementSystem.Services
{
    public class ImageService
    {
        readonly IWebHostEnvironment _iwh;

        public ImageService(IWebHostEnvironment iwh)
        {
            _iwh = iwh;
        }

        public static string imagepath(string imageName)
        {
            return @$"/img/{imageName}";
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

            string Filepath = Path.Combine(_iwh.WebRootPath + "img" + fileName);
            FileStream fileStream = new FileStream(Filepath, FileMode.Create);
            await image.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            fileStream.Close();
            return fileName;
        }

        public bool DeleteImage(string oldFileName)
        {
            string Filepath = Path.Combine(_iwh.WebRootPath, "img", oldFileName);
            FileInfo fi = new FileInfo(Filepath);
            if (File.Exists(fi.ToString()))
            {
                File.Delete(Filepath);
                return true;
            }
            return false;
        }
    }
}

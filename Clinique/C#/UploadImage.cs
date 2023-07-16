namespace AspNetCoreTemplate.C_;

public class UploadImage
{
    public static string UniqueFileName(IFormFile imageFile, string path, IWebHostEnvironment _webHostEnvironment)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, path, uniqueFileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return uniqueFileName;
        }
        return "";
    }
    
}
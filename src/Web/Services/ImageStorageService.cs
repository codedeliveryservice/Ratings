using Web.Interfaces;

namespace Web.Services;

public class ImageStorageService : IImageStorageService
{
    private readonly IWebHostEnvironment _environment;

    public ImageStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string?> UploadImage(IFormFile? formFile)
    {
        // TODO: Move to Configuration file
        const string ImageFolder = "/images/";

        bool isValid = formFile != null && formFile.ContentType.StartsWith("image/");
        if (!isValid)
        {
            return null;
        }

        var directory = _environment.WebRootPath + ImageFolder;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var path = ImageFolder + Guid.NewGuid() + Path.GetExtension(formFile!.FileName);
        using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
        {
            await formFile.CopyToAsync(fileStream);
        }

        return path;
    }
}
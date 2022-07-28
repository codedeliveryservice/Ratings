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
        if (formFile is null)
        {
            return null;
        }

        var isImageType = formFile.ContentType.StartsWith("image/");
        if (!isImageType)
        {
            return null;
        }

        // TODO: Move to Configuration file
        const string ImageFolder = "/images/";

        var directory = _environment.WebRootPath + ImageFolder;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var fileName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
        var fullPath = _environment.WebRootPath + ImageFolder + fileName;

        using var fileStream = new FileStream(fullPath, FileMode.Create);
        await formFile.CopyToAsync(fileStream);

        // NOTE: Return the relative path
        return ImageFolder + fileName;
    }
}
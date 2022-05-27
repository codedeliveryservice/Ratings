namespace Web.Interfaces;

public interface IImageStorageService
{
    Task<string?> UploadImage(IFormFile? formFile);
}
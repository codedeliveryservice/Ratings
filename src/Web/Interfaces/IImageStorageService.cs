namespace Web.Interfaces;

public interface IImageStorageService
{
    /// <summary>
    /// Uploads the image to the server.
    /// If <paramref name="formFile"/> is <see langword="null"/> or the content type isn't an image type, <see langword="null"/> will be returned.
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns>Returns a string containing a relative path to the image or <see langword="null"/>.</returns>
    Task<string?> UploadImage(IFormFile? formFile);
}
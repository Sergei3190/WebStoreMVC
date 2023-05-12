namespace WebStoreMVC.Services.Interfaces;
public interface IFileService
{
    Task SaveFileInRootDirectory(IFormFile formFile, params string[] segments);
}
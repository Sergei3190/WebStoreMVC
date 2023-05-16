using Microsoft.AspNetCore.Http;

namespace WebStoreMVC.Interfaces.Services.Applied;
public interface IFileService
{
    Task SaveFileInRootDirectory(IFormFile formFile, params string[] segments);
}
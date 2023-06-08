using WebStoreMVC.Dto;

namespace WebStoreMVC.Interfaces.Services.Applied;
public interface IFilesService
{
	Task<bool> SaveFileInRootDirectory(FileDto file, CancellationToken cancel = default);
}
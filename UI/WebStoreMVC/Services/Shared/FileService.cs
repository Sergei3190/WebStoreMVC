using System.Text;
using WebStoreMVC.Interfaces.Services.Applied;

namespace WebStoreMVC.Services.Shared
{
    public class FileService : IFileService
	{
		private readonly IWebHostEnvironment _webHost;
		private readonly ILogger<FileService> _logger;

		public FileService(ILogger<FileService> logger, IWebHostEnvironment webHost)
		{
			_logger = logger;
			_webHost = webHost;
		}

		public async Task SaveFileInRootDirectory(IFormFile formFile, params string[] segments)
		{
			var path = GetPath(segments);

			var filePath = Path.Combine(_webHost.WebRootPath, path, formFile.FileName);

			using (var stream = File.Create(filePath))
				await formFile.CopyToAsync(stream);

			_logger.LogInformation("Файл {0} успешно загружен по пути {1}", formFile.FileName, filePath);
		}

		private string GetPath(params string[] segments)
		{
			if (segments is null)
				return string.Empty;

			var path = new StringBuilder();

			foreach (var segment in segments)
				path.Append($"{segment}\\");

			return path.ToString();
		}
	}
}

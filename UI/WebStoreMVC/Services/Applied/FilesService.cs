using System.Text;

using WebStoreMVC.Dto;
using WebStoreMVC.Interfaces.Services.Applied;

namespace WebStoreMVC.Services.Applied
{
	// по хорошему надо вынести реализацию сервиса также через клиента и сохранять картинки в бд, но в рамках данного приложения 
	// основной приоритет демонстрации навыков не на работе с файлами и их хранилищами, поэтому реализация здесь.
	public class FilesService : IFilesService
	{
		private readonly IWebHostEnvironment _webHost;
		private readonly ILogger<FilesService> _logger;

		public FilesService(ILogger<FilesService> logger, IWebHostEnvironment webHost)
		{
			_logger = logger;
			_webHost = webHost;
		}

		public async Task<bool> SaveFileInRootDirectory(FileDto file, CancellationToken cancel = default)
		{
			ArgumentNullException.ThrowIfNull(file, nameof(file));

			var path = GetPath(file.Segments!);

			var filePath = Path.Combine(_webHost.WebRootPath, path, file.FileName);

			if (string.IsNullOrEmpty(filePath))
				return false;

			using (var stream = File.Create(filePath))
				await stream.WriteAsync(file.Content).ConfigureAwait(false);

			_logger.LogInformation("Файл {0} успешно загружен по пути {1}", file.FileName, filePath);

			return true;
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

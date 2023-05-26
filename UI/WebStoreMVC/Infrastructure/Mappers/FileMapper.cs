using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Dto;

namespace WebStoreMVC.Infrastructure.Mappers;

public static class FileMapper
{
	[return: NotNullIfNotNull("file")]
	public static async Task<FileDto?> ToDtoAsync(this IFormFile? file, params string[] segments) => file is null
		? null
		: new FileDto()
		{
			FileName = file.FileName,
			Content = await GetContentAsync(file).ConfigureAwait(false),
			Segments = segments
		};

	private static async Task<byte[]> GetContentAsync(IFormFile file)
	{
		using (var stream = new MemoryStream())
		{
			await file.CopyToAsync(stream).ConfigureAwait(false);
			return stream.ToArray();
		}
	}
}

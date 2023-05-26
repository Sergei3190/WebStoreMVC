namespace WebStoreMVC.Dto;
public class FileDto
{
	public string FileName { get; set; } = null!;

	public byte[] Content { get; set; } = null!;

	public string[]? Segments { get; init; }
}
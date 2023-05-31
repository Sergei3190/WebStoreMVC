namespace WebStore.Domain;

public record Page<T>(IEnumerable<T> Items, int PageNumber, int PageSize, int TotalCount)
{
	public int PageCount => PageSize <= 0 ? -1 : (int)Math.Ceiling((double)TotalCount / PageSize);
}
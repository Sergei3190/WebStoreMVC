namespace WebStoreMVC.Dto
{
	public class EmployeeDto
	{
		public int Id { get; init; }
		public string LastName { get; init; } = null!;
		public string FirstName { get; init; } = null!;
		public string? MiddleName { get; init; }
		public int Age { get; init; }
	}
}

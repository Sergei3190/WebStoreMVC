namespace WebStoreMVC.Interfaces;

public static class WebApiAddresses
{
	public static class V1
	{
		public const string Employees = "api/v1/employees";
		public const string Orders = "api/v1/orders";
		public const string Products = "api/v1/products";
		public const string Blogs = "api/v1/blogs";
		public const string Values = "api/v1/values";

		public static class Identity
		{
			public const string Users = "api/v1/users";
			public const string Roles = "api/v1/roles";
		}
	}
}
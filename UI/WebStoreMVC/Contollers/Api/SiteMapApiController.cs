using Microsoft.AspNetCore.Mvc;

using SimpleMvcSitemap;

using WebStoreMVC.Interfaces.Services;

namespace WebStoreMVC.Controllers.Api;

[ApiController]
[Route("/sitemap")]
public class SiteMapApiController : ControllerBase
{
	[HttpGet]
	public IActionResult Get([FromServices] IProductsService productsService,
		[FromServices] IBlogsService blogsService)
	{
		var nodes = new List<SitemapNode>
		{
			new(Url.Action("Index", "Home")),
			new(Url.Action("Greetings", "Home")),
			new(Url.Action("Contacts", "Home")),
			new(Url.Action("Test", "Home")),
			new(Url.Action("Index", "WebAPI")),
			new(Url.Action("Index", "Catalog")),
		};

		nodes.AddRange(productsService.GetSections().Select(s => new SitemapNode(Url.Action("Index", "Catalog", new { SectionId = s.Id }))));

		foreach (var brand in productsService.GetBrands())
			nodes.Add(new(Url.Action("Index", "Catalog", new { BrandId = brand.Id })));

		foreach (var product in productsService.GetProducts().Items)
			nodes.Add(new(Url.Action("Details", "Catalog", new { product.Id })));

		foreach (var blog in blogsService.GetAll())
			nodes.Add(new(Url.Action("ShopBlog", "Blogs", new { blog.Id })));

		return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
	}
}
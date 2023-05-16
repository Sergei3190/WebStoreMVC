using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.TestApi;

namespace WebStoreMVC.Contollers
{
	public class WebApiController : Controller
	{
		private readonly IValueService _valueService;

		public WebApiController(IValueService valueService)
		{
			_valueService = valueService;
		}

		public IActionResult Index()
		{
			var values = _valueService.GetValues();
			return View(values);
		}
	}
}

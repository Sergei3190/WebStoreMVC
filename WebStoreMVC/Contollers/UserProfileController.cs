using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Controllers;

[Authorize]
public class UserProfileController : Controller
{
	private readonly IProductsService _service;
	private readonly IMapper _mapper;

	public UserProfileController(IProductsService service, IMapper mapper)
	{
		_service = service;
		_mapper = mapper;
	}

	public IActionResult Index()
	{
		return View();
	}

    public async Task<IActionResult> Orders([FromServices] IOrderService service)
    {
		var orders = await service.GetUserOrdersAsync(User!.Identity!.Name!);

        return View(orders.Select(o => new UserOrderViewModel()
		{
			Id = o.Id,
			Date = o.Date,
			Address = o.Address,
			Phone = o.Phone,
			Description = o.Description,	
			TotalPrice = o.TotalPrice
		}));
    }
}
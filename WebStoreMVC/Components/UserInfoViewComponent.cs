using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Components;

public class UserInfoViewComponent : ViewComponent
{
    private readonly IProductsService _service;

    public UserInfoViewComponent(IProductsService service) => _service = service;

    public IViewComponentResult Invoke() => User.Identity!.IsAuthenticated
        ? View("UserInfo")
        : View();
}

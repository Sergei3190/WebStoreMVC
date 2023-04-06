using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogsService _service;

    public BlogsController(IBlogsService service) => _service = service;    

    public IActionResult Index() => View(GetAll());

    public IActionResult ShopBlog(int id) 
    {
        var blog = _service.GetById(id);

        if (blog is null)
            return NotFound();

        return View(MapViewModel(blog));
    }

    private IEnumerable<BlogViewModel> GetAll()
    {
        var blogs = _service.GetAll();

        return blogs
            .OrderBy(b => b.Order) 
            .Select(b => MapViewModel(b));  
    }

    private BlogViewModel MapViewModel(Blog b)
    {
        return new BlogViewModel()
        {
            Id = b.Id,
            Name = b.Name,
            ImageUrl = b.ImageUrl,
            ShortText = b.ShortText,
            FullText = b.FullText,
        };
    }
}

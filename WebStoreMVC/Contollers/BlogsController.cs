using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogsService _service;
    private readonly IMapper _mapper;

    public BlogsController(IBlogsService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public IActionResult Index() => View(GetAll());

    public IActionResult ShopBlog(int? id = null)
    {
        Blog? blog = null;

        if (id is null)
        {
            var blogs = _service.GetAll(isMain: true);

            if (!blogs.Any())
                return NotFound();

            blog = blogs.First();
        }
        else
        {
            blog = _service.GetById(id.Value);

            if (blog is null)
                return NotFound();
        }

        return View(_mapper.Map<BlogViewModel>(blog));
    }

    private IEnumerable<BlogViewModel> GetAll()
    {
        var blogs = _service.GetAll();

        return blogs
            .OrderBy(b => b.Order)
            .Select(b => _mapper.Map<BlogViewModel>(b));
    }
}

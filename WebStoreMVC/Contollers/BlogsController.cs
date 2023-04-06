using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.Controllers;
//TODO
public class BlogsController : Controller
{
    public IActionResult Index() => View(); //должен вернуть представление списка блогов blog.html
    public IActionResult ShopBlog() => View(); //должен вернуть представление блога магазина  blog-single.html
}

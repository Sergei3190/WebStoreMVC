using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Services.Interfaces;
public interface IBlogsService
{
    IEnumerable<Blog> GetAll(bool? isMain = false);

    Blog? GetById(int id);
}
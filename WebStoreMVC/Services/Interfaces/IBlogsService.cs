using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Services.Interfaces;
public interface IBlogsService
{
    IEnumerable<Blog> GetAll();

    Blog? GetById(int id);
}
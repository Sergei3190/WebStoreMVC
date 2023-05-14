using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Interfaces.Services;
public interface IBlogsService
{
    IEnumerable<Blog> GetAll(bool? isMain = false);

    Task<Blog?> GetByIdAsync(int id);
}
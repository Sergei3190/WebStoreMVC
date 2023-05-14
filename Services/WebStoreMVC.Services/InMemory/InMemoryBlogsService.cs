using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Services.Data;

namespace WebStoreMVC.Services.InMemory
{
    public class InMemoryBlogsService : IBlogsService
    {
        public InMemoryBlogsService() { }

        public IEnumerable<Blog> GetAll(bool? isMain) => isMain.HasValue && isMain.Value ? TestData.Blogs.Where(b => b.IsMain) : TestData.Blogs;

        public async Task<Blog?> GetByIdAsync(int id) => await Task.FromResult(TestData.Blogs.FirstOrDefault(b => b.Id == id)).ConfigureAwait(false);
    }
}

using WebStoreMVC.Data;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Services
{
    public class InMemoryBlogsService : IBlogsService
    {
        public InMemoryBlogsService() { }

        public IEnumerable<Blog> GetAll() => TestData.Blogs;

        public Blog? GetById(int id) => TestData.Blogs.FirstOrDefault(b => b.Id == id);
    }
}

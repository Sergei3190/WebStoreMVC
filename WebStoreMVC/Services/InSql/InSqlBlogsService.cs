﻿using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Services.InSql
{
    public class InSqlBlogsService : IBlogsService
    {
        private readonly WebStoreMVC_DB _db;

        public InSqlBlogsService(WebStoreMVC_DB db)
        {
            _db = db;
        }

        public IEnumerable<Blog> GetAll(bool? isMain) => isMain.HasValue && isMain.Value ? _db.Blogs.Where(b => b.IsMain) : _db.Blogs;

        public async Task<Blog?> GetByIdAsync(int id) => await _db.Blogs.FirstOrDefaultAsync(b => b.Id == id).ConfigureAwait(false);
    }
}

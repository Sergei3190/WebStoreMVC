using AutoMapper;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Services.Mappers.AutoMappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<Blog, BlogViewModel>()
            .ReverseMap();
    }
}
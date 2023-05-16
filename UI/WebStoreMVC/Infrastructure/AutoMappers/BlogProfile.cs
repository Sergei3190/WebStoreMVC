using AutoMapper;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Infrastructure.AutoMappers;

public class BlogProfile : Profile
{
	public BlogProfile()
	{
		CreateMap<Blog, BlogViewModel>()
			.ReverseMap();
	}
}
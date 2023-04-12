using AutoMapper;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Infrastructure.AutoMappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductViewModel>();
    }
}
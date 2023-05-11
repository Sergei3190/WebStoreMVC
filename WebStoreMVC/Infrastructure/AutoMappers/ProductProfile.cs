using AutoMapper;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Infrastructure.AutoMappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(vm => vm.Brand, m => m.MapFrom(m => m.Brand != null ? m.Brand.Name : null))
            .ForMember(vm => vm.Section, m => m.MapFrom(m => m.Section!.Name));
    }
}
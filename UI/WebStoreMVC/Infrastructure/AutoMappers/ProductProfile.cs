using AutoMapper;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Infrastructure.AutoMappers;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductViewModel>()
			.ForMember(vm => vm.Brand, m => m.MapFrom(m => m.Brand != null ? new BrandViewModel()
			{
				Id = m.Brand.Id,
				Name = m.Brand.Name
			} : null))
			.ForMember(vm => vm.Section, m => m.MapFrom(m => new SectionViewModel()
			{
				Id = m.Section.Id,
				Name = m.Section.Name
			}));

		CreateMap<ProductViewModel, Product>()
			.ForMember(m => m.Section, vm => vm.Ignore())
			.ForMember(m => m.Brand, vm => vm.Ignore());
	}
}
using AutoMapper;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Infrastructure.AutoMappers;

public class EmployeeProfile : Profile
{
	public EmployeeProfile()
	{
		CreateMap<Employee, EmployeeViewModel>()
			.ReverseMap();
	}
}
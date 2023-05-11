using System.ComponentModel.DataAnnotations;

namespace WebStoreMVC.ViewModels;

public class OrderViewModel
{
	public OrderViewModel()
	{
		Address = null!;
		Phone = null!;
	}

	[Required(ErrorMessage = "Не заполнен адрес заказа"), MaxLength(200)]
	[Display(Name = "Адрес заказа")]
	public string Address { get; set; }

	[Required(ErrorMessage = "Не заполнен телефон"), MaxLength(200)]
	[Display(Name = "Телефон")]
	[DataType(DataType.PhoneNumber)]
	public string Phone { get; set; }

	[MaxLength(200)]
	[Display(Name = "Комментарий")]
	[DataType(DataType.MultilineText)]
	public string? Description { get; set; }
}
using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Interfaces.Services.Applied;
public interface ICartStore
{
	public Cart Cart { get; set; }
}
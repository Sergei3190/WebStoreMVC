using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WebStoreMVC.Domain.Entities.Base;

namespace WebStoreMVC.Domain.Entities.Orders;

public class OrderItem : Entity
{
    public OrderItem()
    {
        Product = null!;
        Order = null!;
    }

    [Required]
    public Product Product { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [Required]
    public Order Order { get; set; }

    [NotMapped]
    public decimal TotalItemPrice => Price * Quantity;
}

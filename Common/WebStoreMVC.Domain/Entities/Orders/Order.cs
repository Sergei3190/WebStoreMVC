using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WebStoreMVC.Domain.Entities.Base;
using WebStoreMVC.Domain.Entities.Identity;

namespace WebStoreMVC.Domain.Entities.Orders;

public class Order : Entity
{
    public Order()
    {
        User = null!;
        Phone = null!;
        Address = null!;
        Date = DateTimeOffset.Now;
        Items = new HashSet<OrderItem>();
    }

    [Required]
    public User User { get; set; }

    [Required]
    [MaxLength(200)]
    public string Phone { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset Date { get; set; }

    public ICollection<OrderItem> Items { get; set; }

    [NotMapped]
    public decimal TotalPrice => Items.Sum(i => i.TotalItemPrice);
}

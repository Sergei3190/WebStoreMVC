using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Models;

namespace WebStoreMVC.Data;

public static class TestData
{
    public static ICollection<Employee> Employees { get; } = new List<Employee>()
    {
        new () { Id = 1, Age = 23, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович" },
        new () { Id = 2, Age = 27, LastName = "Петров", FirstName = "Петр", MiddleName = "Петрович" },
        new () { Id = 3, Age = 18, LastName = "Сидоров", FirstName = "Сидор", MiddleName = "Сидорович" }
    };

    public static IEnumerable<Section> Sections { get; } = new List<Section>()
    {
        new () { Id = 1, Name = "Спорт", Order = 0, ParentId = null},
        new () { Id = 2, Name = "Nike", Order = 0, ParentId = 1},
        new () { Id = 3, Name = "Under Armour", Order = 1, ParentId = 1},
        new () { Id = 4, Name = "Adidas", Order = 2, ParentId = 1},
        new () { Id = 5, Name = "Puma", Order = 3, ParentId = 1},
        new () { Id = 6, Name = "ASICS", Order = 4, ParentId = 1},
        new () { Id = 7, Name = "Для мужчин", Order = 1, ParentId = null},
        new () { Id = 8, Name = "Fendi", Order = 0, ParentId = 7},
        new () { Id = 9, Name = "Guess", Order = 1, ParentId = 7},
        new () { Id = 10, Name = "Valentino", Order = 2, ParentId = 7},
        new () { Id = 11, Name = "Dior", Order = 3, ParentId = 7},
        new () { Id = 12, Name = "Versace", Order = 4, ParentId = 7},
        new () { Id = 13, Name = "Armani", Order = 5, ParentId = 7},
        new () { Id = 14, Name = "Prada", Order = 6, ParentId = 7},
        new () { Id = 15, Name = "Dolce and Gabbana", Order = 7, ParentId = 7},
        new () { Id = 16, Name = "Chanel", Order = 8, ParentId = 7},
        new () { Id = 17, Name = "Gucci", Order = 9, ParentId = 7},
        new () { Id = 18, Name = "Для женщин", Order = 2, ParentId = null},
        new () { Id = 19, Name = "Fendi", Order = 0, ParentId = 18},
        new () { Id = 20, Name = "Guess", Order = 1, ParentId = 18},
        new () { Id = 21, Name = "Valentino", Order = 2, ParentId = 18},
        new () { Id = 22, Name = "Dior", Order = 3, ParentId = 18},
        new () { Id = 23, Name = "Versace", Order = 4, ParentId = 18},
        new () { Id = 24, Name = "Для детей", Order = 3, ParentId = null},
        new () { Id = 25, Name = "Мода", Order = 4, ParentId = null},
        new () { Id = 26, Name = "Для дома", Order = 5, ParentId = null},
        new () { Id = 27, Name = "Интерьер", Order = 6, ParentId = null},
        new () { Id = 28, Name = "Одежда", Order = 7, ParentId = null},
        new () { Id = 29, Name = "Сумки", Order = 8, ParentId = null},
        new () { Id = 30, Name = "Обувь", Order = 9, ParentId = null},
    };

    public static IEnumerable<Brand> Brands { get; } = new List<Brand>()
    {
        new () { Id = 1, Name = "Acne", Order = 0},
        new () { Id = 2, Name = "Grüne Erde", Order = 1},
        new () { Id = 3, Name = "Albiro", Order = 2},
        new () { Id = 4, Name = "Ronhill", Order = 3},
        new () { Id = 5, Name = "Oddmolly", Order = 4},
        new () { Id = 6, Name = "Boudestijn", Order = 5},
        new () { Id = 7, Name = "Rösch creative culture", Order = 6},
    };

    public static IEnumerable<Product> Products { get; } = new List<Product>()
    {
        new () { Id = 1, Name = "Easy Polo Black Edition", Order = 0, SectionId = 2, BrandId = 1, ImageUrl = "product1.jpg", Price = 1025,},
        new () { Id = 2, Name = "Easy Polo Black Edition", Order = 1, SectionId = 2, BrandId = 1, ImageUrl = "product2.jpg", Price = 1025,},
        new () { Id = 3, Name = "Easy Polo Black Edition", Order = 2, SectionId = 2, BrandId = 1, ImageUrl = "product3.jpg", Price = 1025,},
        new () { Id = 4, Name = "Easy Polo Black Edition", Order = 3, SectionId = 2, BrandId = 1, ImageUrl = "product4.jpg", Price = 1025,},
        new () { Id = 5, Name = "Easy Polo Black Edition", Order = 4, SectionId = 2, BrandId = 1, ImageUrl = "product5.jpg", Price = 1025,},
        new () { Id = 6, Name = "Easy Polo Black Edition", Order = 5, SectionId = 2, BrandId = 2, ImageUrl = "product6.jpg", Price = 1025,},
        new () { Id = 7, Name = "Easy Polo Black Edition", Order = 6, SectionId = 2, BrandId = 2, ImageUrl = "product7.jpg", Price = 1025,},
        new () { Id = 8, Name = "Easy Polo Black Edition", Order = 7, SectionId = 25, BrandId = 2, ImageUrl = "product8.jpg", Price = 1025,},
        new () { Id = 9, Name = "Easy Polo Black Edition", Order = 8, SectionId = 25, BrandId = 2, ImageUrl = "product9.jpg", Price = 1025,},
        new () { Id = 10, Name = "Easy Polo Black Edition", Order = 9, SectionId = 25, BrandId = 3, ImageUrl = "product10.jpg", Price = 1025,},
        new () { Id = 11, Name = "Easy Polo Black Edition", Order = 10, SectionId = 25, BrandId = 3, ImageUrl = "product11.jpg", Price = 1025,},
        new () { Id = 12, Name = "Easy Polo Black Edition", Order = 11, SectionId = 25, BrandId = 3, ImageUrl = "product12.jpg", Price = 1025,},
    };
}
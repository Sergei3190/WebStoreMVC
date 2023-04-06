using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Models;

namespace WebStoreMVC.Data;

public static class TestData
{
    private static readonly string _productName = "Easy Polo Black Edition";
    private static readonly string _blogName = "Girls Pink T Shirt arrived in store";
    private static readonly string _shortText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";
    private static readonly string _fullText = @"
    <p>
        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.</p> <br>
    <p>
        Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p> <br>
    <p>
        Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.</p> <br>
    <p>
        Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.
    </p>";

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
        new () { Id = 1, Name = _productName, Order = 0, SectionId = 2, BrandId = 1, ImageUrl = "product1.jpg", Price = 1025,},
        new () { Id = 2, Name = _productName, Order = 1, SectionId = 2, BrandId = 1, ImageUrl = "product2.jpg", Price = 1025,},
        new () { Id = 3, Name = _productName, Order = 2, SectionId = 2, BrandId = 1, ImageUrl = "product3.jpg", Price = 1025,},
        new () { Id = 4, Name = _productName, Order = 3, SectionId = 2, BrandId = 1, ImageUrl = "product4.jpg", Price = 1025,},
        new () { Id = 5, Name = _productName, Order = 4, SectionId = 2, BrandId = 1, ImageUrl = "product5.jpg", Price = 1025,},
        new () { Id = 6, Name = _productName, Order = 5, SectionId = 2, BrandId = 2, ImageUrl = "product6.jpg", Price = 1025,},
        new () { Id = 7, Name = _productName, Order = 6, SectionId = 2, BrandId = 2, ImageUrl = "product7.jpg", Price = 1025,},
        new () { Id = 8, Name = _productName, Order = 7, SectionId = 25, BrandId = 2, ImageUrl = "product8.jpg", Price = 1025,},
        new () { Id = 9, Name = _productName, Order = 8, SectionId = 25, BrandId = 2, ImageUrl = "product9.jpg", Price = 1025,},
        new () { Id = 10, Name = _productName, Order = 9, SectionId = 25, BrandId = 3, ImageUrl = "product10.jpg", Price = 1025,},
        new () { Id = 11, Name = _productName, Order = 10, SectionId = 25, BrandId = 3, ImageUrl = "product11.jpg", Price = 1025,},
        new () { Id = 12, Name = _productName, Order = 11, SectionId = 25, BrandId = 3, ImageUrl = "product12.jpg", Price = 1025,},
    };

    public static IEnumerable<Blog> Blogs { get; } = new List<Blog>()
    {
        new ()
        {
            Id = 1,
            Name = _blogName,
            Order = 0,
            IsMain = true,
            ImageUrl = "blog-one.jpg",
            ShortText = _shortText,
            FullText = _fullText,
        },
        new ()
        {
            Id = 2,
            Name = _blogName,
            Order = 1,
            IsMain = false,
            ImageUrl = "blog-two.jpg",
            ShortText = _shortText,
            FullText = _fullText,
        },
        new ()
        {
            Id = 3,
            Name = _blogName,
            Order = 2,
            IsMain = false,
            ImageUrl = "blog-three.jpg",
            ShortText = _shortText,
            FullText = _fullText,
        },
    };
}
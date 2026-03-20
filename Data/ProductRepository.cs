namespace ProductCatalogApi.Data;

public class ProductRepository
{
    private readonly List<Product> _products =
    [
        new Product(1,"Laptop",1200m,"Electronics"),
        new Product(2,"Headphones",150m,"Electronics"),
        new Product(3,"Desk Chair",300m,"Furniture")
    ];

    
    private record Product(int Id,string Name,decimal Price,string Category);
}

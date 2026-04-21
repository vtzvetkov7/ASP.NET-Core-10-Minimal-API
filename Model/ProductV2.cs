namespace ProductCatalogApi.Models;
public record ProductV2(
    int Id,
    string Name,
    decimal Price,
    string Category
);
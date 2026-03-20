using ProductCatalogApi.Data;
using ProductCatalogApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Product Catalog API is running");

app.MapGet("/api/products", (ProductRepository repo) =>
{
    var products = repo.GetAll()
        .Select(p => new ProductV1(p.Id, p.Name, p.Price));

    return Results.Ok(products);
})
.WithName("GetProducts")
.Produces<IEnumerable<ProductV1>>(StatusCodes.Status200OK);

app.MapGet("/api/products/{id:int}", (int id, ProductRepository repo) =>
{
    var product = repo.GetById(id);

    if (product is null)
        return Results.NotFound();

    return Results.Ok(new ProductV1(product.Id, product.Name, product.Price));
})
.WithName("GetProductById")
.Produces<ProductV1>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);


app.Run();

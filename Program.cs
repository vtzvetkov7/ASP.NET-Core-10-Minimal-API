using Asp.Versioning;
using ProductCatalogApi.Data;
using ProductCatalogApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


// Mapping section
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


// Versioning section
var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1,0))
    .HasApiVersion(new ApiVersion(2,0))
    .Build();

var v1 = app.MapGroup("/api/v{version:apiVersion}/products")
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(1.0);

var v2 = app.MapGroup("/api/v{version:apiVersion}/products")
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(2.0);

v1.MapGet("/", (ProductRepository repo) =>
{
    var products = repo.GetAll()
        .Select(p => new ProductV1(p.Id, p.Name, p.Price));

    return Results.Ok(products);
})
.WithName("GetProductsV1")
.Produces<IEnumerable<ProductV1>>(200);


v2.MapGet("/", (ProductRepository repo, string? category) =>
{
    var products = repo.GetAll();

    if (!string.IsNullOrEmpty(category))
        products = products.Where(p => p.Category == category);

    var result = products.Select(p =>
        new ProductV2(p.Id, p.Name, p.Price, p.Category));

    return Results.Ok(result);
})
.WithName("GetProductsV2")
.Produces<IEnumerable<ProductV2>>(200);


app.Run();

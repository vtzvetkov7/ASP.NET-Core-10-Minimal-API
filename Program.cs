using ProductCatalogApi.Data;
using ProductCatalogApi.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Product Catalog API is running");

app.Run();
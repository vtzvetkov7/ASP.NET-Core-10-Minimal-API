using Asp.Versioning;
using ProductCatalogApi.Data;
using ProductCatalogApi.Models;

var builder = WebApplication.CreateBuilder(args);

// This section is for WebApplicationBuilder class (builder object)
// Put here statements for Swagger generation, repository registration or API versioning 


var app = builder.Build();

// Mapping section
app.MapGet("/", () => "Product Catalog API is running");


// Versioning section


app.Run();

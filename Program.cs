using Asp.Versioning;
using ProductCatalogApi.Data;
using ProductCatalogApi.Models;

var builder = WebApplication.CreateBuilder(args);

// This section is for WebApplicationBuilder class (builder object)
// Put here statements for Swagger generation, repository registration or API versioning 


var app = builder.Build();

// Mapping section




// Versioning section


app.Run();

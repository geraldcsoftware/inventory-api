using FastEndpoints;
using Inventory.Api;
using Inventory.Api.Infrastructure;
using Inventory.Data;
using Inventory.Data.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);
builder.SetupLogger();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints()
       .AddResponseCaching()
       .AddResponseCompression();

builder.Services.AddDbInitializer();

builder.Services.AddInventoryPostgresDbContext();
builder.Services.AddInventoryRepositories();

builder.Services.AddAuthentication().AddJwtBearer();
var app = builder.Build();

await TryRunInitializers(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.UseFastEndpoints();
app.UseResponseCompression();
app.Run();
return;

async Task TryRunInitializers(WebApplication webApplication)
{
    try
    {
        await webApplication.InitializeAsync();
    }
    catch (Exception exception)
    {
        webApplication.Logger.LogCritical(exception, "An error occurred while migrating or initializing the application components");
        throw;
    }
}
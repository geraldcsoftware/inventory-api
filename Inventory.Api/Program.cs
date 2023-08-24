using FastEndpoints;
using Inventory.Api;

var builder = WebApplication.CreateBuilder(args);
builder.SetupLogger();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddFastEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

//app.UseFastEndpoints();
app.MapGet("info", (ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("InfoEndpoint");
    logger.LogInformation("We hit the info endpoint at {Time}", DateTime.UtcNow);
    return Results.Ok(new { success = true, message = "Hello" });
});
app.Run();
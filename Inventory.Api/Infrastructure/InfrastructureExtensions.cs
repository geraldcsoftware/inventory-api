namespace Inventory.Api.Infrastructure;

public static class InfrastructureExtensions
{
    public static void AddDbInitializer(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient<IAppInitializer, DatabaseInitializer>();
    }

    public static async ValueTask InitializeAsync(this WebApplication app, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(app);
        var initializers = app.Services.GetServices<IAppInitializer>();

        foreach (var initializer in initializers)
        {
            await initializer.InitializeAsync(cancellationToken);
        }
    }
}
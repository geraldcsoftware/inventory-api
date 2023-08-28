namespace Inventory.Api.Infrastructure;

public interface IAppInitializer
{
    ValueTask InitializeAsync(CancellationToken cancellationToken = default);
}
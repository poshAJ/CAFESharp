using CAFESharp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CAFESharp.Extensions;

public static class IServiceCollectionExtensions {
    public static IServiceCollection AddViewModels (
        this IServiceCollection serviceCollection
    ) {
        serviceCollection.TryAddTransient<BlueprintViewModel>();
        serviceCollection.TryAddTransient<WeaponViewModel>();
        serviceCollection.TryAddTransient<MaterialInstanceViewModel>();
        serviceCollection.TryAddTransient<BloodSplatterViewModel>();

        serviceCollection.TryAddTransient<MainViewModel>();

        return serviceCollection;
    }
}

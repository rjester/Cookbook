using Autofac;
using Cookbook.Core.Interfaces;
using Cookbook.Core.Services;

namespace Cookbook.Core;

    public class DefaultCoreModule : Module
    {
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RecipeService>()
            .As<IRecipeService>().InstancePerLifetimeScope();
    }
}


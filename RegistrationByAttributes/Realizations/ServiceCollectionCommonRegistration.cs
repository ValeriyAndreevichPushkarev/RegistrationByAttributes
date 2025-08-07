using Microsoft.Extensions.DependencyInjection;
using RegistrationByAttributes.Attributes;

namespace RegistrationByAttributes.Realizations
{
    /// <summary>
    /// Registers all types with attributes in container
    /// </summary>
    public class DependencyInjectionCommonRegistration : CommonRegistration<IServiceCollection>
    {
        protected override void registerInContainer(IServiceCollection container, Type baseType, LifetimeManagementType lifetimeManagement)
        {
            switch(lifetimeManagement)
            {
                case LifetimeManagementType.Default:
                    container.AddScoped(baseType);
                    break;
                case LifetimeManagementType.Singletone:
                    container.AddSingleton(baseType);
                    break;
                case LifetimeManagementType.PerResolve:
                    container.AddTransient(baseType);
                    break;
                case LifetimeManagementType.PerThread:
                    container.AddScoped(baseType);
                    break;
            }
        }

        protected override void registerInContainer(IServiceCollection container, Type baseType, Type derivedType, LifetimeManagementType lifetimeManagement)
        {
            switch (lifetimeManagement)
            {
                case LifetimeManagementType.Default:
                    container.AddScoped(baseType, derivedType);
                    break;
                case LifetimeManagementType.Singletone:
                    container.AddSingleton(baseType, derivedType);
                    break;
                case LifetimeManagementType.PerResolve:
                    container.AddTransient(baseType, derivedType);
                    break;
                case LifetimeManagementType.PerThread:
                    container.AddScoped(baseType, derivedType);
                    break;
            }
        }

        protected override void registerManyInContainer(IServiceCollection container, Type typeForRegistration1, Type typeForRegistration2, LifetimeManagementType lifetimeManagement)
        {
            registerInContainer(container, typeForRegistration1, typeForRegistration2, LifetimeManagementType.Default);
        }
    }
}

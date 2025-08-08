using RegistrationByAttributes.Attributes;
using Unity;
using Unity.Lifetime;

namespace RegistrationByAttributes.Realizations
{
    /// <summary>
    /// Registers all types with attributes in container
    /// </summary>
    public class UnityCommonRegistration : CommonRegistration<IUnityContainer>
    {
        protected override void registerInContainer(IUnityContainer container, Type typeForRegistration1, Type typeForRegistration2, LifetimeManagementType lifetimeManagement, string key = null)
        {
            var ltm = getLifetimeManagement(lifetimeManagement);

            if (key==null)
            {
                container.RegisterType(typeForRegistration1, typeForRegistration2, ltm);
            }
            else
            {
                container.RegisterType(typeForRegistration1, typeForRegistration2, key, ltm);
            }
            
        }

        protected override void registerInContainer(IUnityContainer container, Type typeForRegistration1, LifetimeManagementType lifetimeManagement, string key = null)
        {
            var ltm = getLifetimeManagement(lifetimeManagement);

            if (key == null)
            {
                container.RegisterType(typeForRegistration1, ltm);
            }
            else
            {
                container.RegisterType(typeForRegistration1, key, ltm);
            }
        }

        protected override void registerManyInContainer(IUnityContainer container, Type typeForRegistration1, Type typeForRegistration2, LifetimeManagementType lifetimeManagement)
        {
            container.RegisterType(typeForRegistration1, typeForRegistration2, typeForRegistration2.Name);
        }

        private ITypeLifetimeManager getLifetimeManagement(LifetimeManagementType lifetimeManagement)
        {
            switch (lifetimeManagement)
            {
                case LifetimeManagementType.Singletone:
                    return new SingletonLifetimeManager();
                case LifetimeManagementType.PerThread:
                    return new PerThreadLifetimeManager();
                case LifetimeManagementType.PerResolve:
                    return new PerResolveLifetimeManager();
                default:
                    return new PerThreadLifetimeManager();
            }
        }
    }
}

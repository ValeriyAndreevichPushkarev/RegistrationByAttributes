﻿using RegistrationByAttributes.Attributes;
using Unity;
using Unity.Lifetime;

namespace RegistrationByAttributes.Realizations
{
    /// <summary>
    /// Registers all types with attributes in container
    /// </summary>
    public class UnityCommonRegistration : CommonRegistration<UnityContainer>
    {
        protected override void registerInContainer(UnityContainer container, Type typeForRegistration1, Type typeForRegistration2, LifetimeManagementType lifetimeManagement)
        {
            var ltm = getLifetimeManagement(lifetimeManagement);

            container.RegisterType(typeForRegistration1, typeForRegistration2, ltm);
        }

        protected override void registerInContainer(UnityContainer container, Type typeForRegistration1, LifetimeManagementType lifetimeManagement)
        {
            var ltm = getLifetimeManagement(lifetimeManagement);

            container.RegisterType(typeForRegistration1, ltm);
        }

        protected override void registerManyInContainer(UnityContainer container, Type typeForRegistration1, Type typeForRegistration2, LifetimeManagementType lifetimeManagement)
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

using Microsoft.Extensions.DependencyInjection;
using RegistrationByAttributes.Realizations;
using System.Reflection;
using Unity;

namespace RegistrationByAttributes
{
    /// <summary>
    /// Extention methods for containers
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Register all types from executing assembly with attributes
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterByAttributes(this IServiceCollection collection)
        {
            DependencyInjectionCommonRegistration reg = new DependencyInjectionCommonRegistration();

            reg.Register(collection, Assembly.GetCallingAssembly());

            return collection;
        }

        /// <summary>
        /// Register all services from specified assembly
        /// </summary>
        /// <param name="collection">Service collection</param>
        /// <param name="assembly">specified assembly</param>
        /// <returns></returns>
        public static IServiceCollection RegisterByAttributes(this IServiceCollection collection, Assembly assembly)
        {
            DependencyInjectionCommonRegistration reg = new DependencyInjectionCommonRegistration();

            reg.Register(collection, assembly);

            return collection;
        }

        /// <summary>
        /// Register all types from executing assembly with attributes
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static IUnityContainer RegisterByAttributes(this IUnityContainer container)
        {
            UnityCommonRegistration reg = new UnityCommonRegistration();

            reg.Register(container, Assembly.GetCallingAssembly());

            return container;
        }

        /// <summary>
        /// Register all services from specified assembly
        /// </summary>
        /// <param name="collection">Service collection</param>
        /// <param name="assembly">specified assembly</param>
        /// <returns></returns>
        public static IUnityContainer RegisterByAttributes(this IUnityContainer collection, Assembly assembly)
        {
            UnityCommonRegistration reg = new UnityCommonRegistration();

            reg.Register(collection, assembly);

            return collection;
        }
    }
}

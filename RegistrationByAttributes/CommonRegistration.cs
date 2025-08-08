using RegistrationByAttributes.Attributes;
using RegistrationByAttributes.Data;
using System.IO;
using System.Reflection;
using Unity;

namespace RegistrationByAttributes
{
    public abstract class CommonRegistration<T>
        where T : class
    {
        /// <summary>
        /// Register all types from calling assembly
        /// </summary>
        /// <param name="container">container for registration</param>
        public void Register(T container)
        {
            var assembly = Assembly.GetCallingAssembly();

            var types = GetTypesWithAttributes<TypeRegistrationAttribute>(assembly);
            var DerivedTypes = GetAllDerivedTypes(types, assembly);

            registerAllTypes(container, DerivedTypes);
        }

        public void Register(T container, Assembly assembly)
        {
            var types = GetTypesWithAttributes<TypeRegistrationAttribute>(assembly);
            var DerivedTypes = GetAllDerivedTypes(types, assembly);

            registerAllTypes(container, DerivedTypes);
        }
        /// <summary>
        /// Register all types from another assembly
        /// </summary>
        /// <param name="container">container for registration</param>
        /// <param name="assembly">Another assembly</param>
        public void RegisterFromAnotherAssembly(T container, Assembly assembly)
        {
            var types = GetTypesWithAttributes<TypeRegistrationAttribute>(assembly);
            var DerivedTypes = GetAllDerivedTypes(types, assembly);

            registerAllTypes(container, DerivedTypes);
        }

        private void registerAllTypes(T container, Dictionary<TypeAndAttributeData<TypeRegistrationAttribute>, List<TypeAndAttributeData<DerivedTypeRegistrationAttribute>>> derivedTypes)
        {
            foreach (var baseType in derivedTypes.Keys)
            {
                var lifetimeManagement = baseType.AttributeType.lifetimeManagementType;



                foreach (var derivedType in derivedTypes[baseType])
                {
                    //If attribute on class is present
                    //Then, change lifetimanagement if its not default
                    if (derivedType.AttributeType != null)
                    {
                        if (derivedType.AttributeType.lifetimeManagementType != LifetimeManagementType.Default)
                            lifetimeManagement = derivedType.AttributeType.lifetimeManagementType;
                    }
                    
                    if (baseType.TypeForRegistration == derivedType.TypeForRegistration)
                        continue;
                    //Generics
                    if (baseType.TypeForRegistration.IsGenericType)
                    {
                        var iface = derivedType.TypeForRegistration.GetInterfaces().First(i =>  i == baseType.TypeForRegistration);

                        if (derivedType.TypeForRegistration.IsGenericType)
                        {
                            registerInContainer(container, iface.GetGenericTypeDefinition(), derivedType.TypeForRegistration.GetGenericTypeDefinition(), lifetimeManagement, derivedType?.AttributeType?.name);
                        }
                        else
                        {
                            registerInContainer(container, iface, derivedType.TypeForRegistration, lifetimeManagement, derivedType?.AttributeType?.name);
                        }

                        continue;
                    }

                    if (typeof(T) is IUnityContainer)
                    {
                        if (derivedTypes[baseType].Count > 1)
                            registerManyInContainer(container, baseType.TypeForRegistration, derivedType.TypeForRegistration, lifetimeManagement);
                        else
                            registerInContainer(container, baseType.TypeForRegistration, derivedType.TypeForRegistration, lifetimeManagement, derivedType?.AttributeType?.name);
                    }
                    else
                        registerInContainer(container, baseType.TypeForRegistration, derivedType.TypeForRegistration, lifetimeManagement, derivedType?.AttributeType?.name);

                }
            }
        }

        /// <summary>
        /// Register single type in container
        /// </summary>
        /// <param name="container">container for registration</param>
        /// <param name="baseType">single type for registration</param>
        /// <param name="lifetimeManagement">lifetime management setting</param>
        protected abstract void registerInContainer(T container, Type baseType, LifetimeManagementType lifetimeManagement, string key = null);

        /// <summary>
        /// Register base type and realization in container
        /// </summary>
        /// <param name="container">container for registration</param>
        /// <param name="baseType">base type for registration(usually interface)</param>
        /// <param name="derivedType">realization type for registration</param>
        /// <param name="lifetimeManagement">lifetime management setting</param>
        protected abstract void registerInContainer(T container, Type baseType, Type derivedType, LifetimeManagementType lifetimeManagement, string key = null);

        /// <summary>
        /// Register base type and many realizations in container
        /// </summary>
        /// <param name="container">container for registration</param>
        /// <param name="baseType">base type for registration(usually interface)</param>
        /// <param name="derivedType">realization type for registration</param>
        /// <param name="lifetimeManagement">lifetime management setting</param>
        protected abstract void registerManyInContainer(T container, Type baseType, Type derivedType, LifetimeManagementType lifetimeManagement);

        private List<TypeAndAttributeData<T>> GetTypesWithAttributes<T>(Assembly assembly)
            where T : Attribute
        {
            var implementedInterfaces = assembly.GetTypes()
                .Where(item => !item.IsAbstract && !item.IsInterface)
                .SelectMany(i=>i.GetInterfaces().Where(iface=>iface.GetCustomAttributes(false).Any(item2 => item2 is T))).Distinct().ToList();

            return implementedInterfaces
                .Select(item => new TypeAndAttributeData<T>
                {
                    AttributeType = item.GetCustomAttribute<T>(false),
                    TypeForRegistration = item

                })
                .ToList();
        }

        private List<TypeAndAttributeData<T>> GetDerivedTypes<T>(Type type, Assembly assembly)
            where T : Attribute
        {
            return assembly.GetTypes()
                .Where(item =>  !item.IsAbstract && !item.IsInterface && 
                    (
                    item.GetInterfaces().Any(i => (!i.IsGenericType && i== type) ||
                                                  (i.IsGenericType && i.GetGenericTypeDefinition()== type) ||
                                                  (i.IsGenericType && i == type) 
                                                  )
                    )
                )
                .Select(item => new TypeAndAttributeData<T>
                {
                    AttributeType = item.GetCustomAttribute<T>(false),
                    TypeForRegistration = item

                })
            .ToList();
        }
        private Dictionary<TypeAndAttributeData<TypeRegistrationAttribute>, List<TypeAndAttributeData<DerivedTypeRegistrationAttribute>>> GetAllDerivedTypes(List<TypeAndAttributeData<TypeRegistrationAttribute>> baseTypes, Assembly assembly)
        {
            var res = new Dictionary<TypeAndAttributeData<TypeRegistrationAttribute>, List<TypeAndAttributeData<DerivedTypeRegistrationAttribute>>>();

            foreach (var type in baseTypes)
            {
                var derivedTypes = GetDerivedTypes<DerivedTypeRegistrationAttribute>(type.TypeForRegistration, assembly);

                res.Add(type, derivedTypes);
            }

            return res;
        }
    }
}

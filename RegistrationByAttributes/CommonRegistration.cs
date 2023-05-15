using RegistrationByAttributes.Attributes;
using RegistrationByAttributes.Data;
using System.Reflection;

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
                    if (derivedType.AttributeType != null)
                    {
                        lifetimeManagement = derivedType.AttributeType.lifetimeManagementType;
                    }
                    
                    if (baseType.TypeForRegistration == derivedType.TypeForRegistration)
                        continue;

                    if (derivedTypes[baseType].Count > 1)
                        registerManyInContainer(container, baseType.TypeForRegistration, derivedType.TypeForRegistration, lifetimeManagement);
                    else
                        registerInContainer(container, baseType.TypeForRegistration, derivedType.TypeForRegistration, lifetimeManagement);
                }
                //container.RegisterType<DataRenderer>();
                if (derivedTypes[baseType].Count()==0)
                {
                    registerInContainer(container, baseType.TypeForRegistration, lifetimeManagement); 
                }
            }
        }

        /// <summary>
        /// Register single type in container
        /// </summary>
        /// <param name="container">container for registration</param>
        /// <param name="baseType">single type for registration</param>
        /// <param name="lifetimeManagement">lifetime management setting</param>
        protected abstract void registerInContainer(T container, Type baseType, LifetimeManagementType lifetimeManagement);

        /// <summary>
        /// Register base type and realization in container
        /// </summary>
        /// <param name="container">container for registration</param>
        /// <param name="baseType">base type for registration(usually interface)</param>
        /// <param name="derivedType">realization type for registration</param>
        /// <param name="lifetimeManagement">lifetime management setting</param>
        protected abstract void registerInContainer(T container, Type baseType, Type derivedType, LifetimeManagementType lifetimeManagement);

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
            var typesWithAttributes = assembly.GetTypes();
            typesWithAttributes = typesWithAttributes
                .Where(item => item.GetCustomAttributes(false).Any(item2 => item2 is T))
                .ToArray();

            return typesWithAttributes
                .Select(item => new TypeAndAttributeData<T>
                {
                    AttributeType = item.GetCustomAttributes<T>(false)
                    .Where(item2 => item2 is T)
                    .Select(item2 => item2 as T)
                    .FirstOrDefault(),
                    TypeForRegistration = item

                })
                .ToList();
        }

        private List<TypeAndAttributeData<T>> GetDerivedTypes<T>(Type type, Assembly assembly)
            where T : Attribute
        {
            return assembly.GetTypes()
                .Where(item => type.IsAssignableFrom(item))
                .Select(item => new TypeAndAttributeData<T>
                {
                    AttributeType = item.GetCustomAttributes<T>(false)
                .Where(item2 => item2 is T)
                .Select(item2 => item2 as T)
                .FirstOrDefault(),
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

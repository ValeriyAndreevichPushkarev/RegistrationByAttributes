using RegistrationByAttributes.Attributes;
using RegistrationByAttributes.Data;
using System.Reflection;

namespace RegistrationByAttributes
{
    public abstract class CommonRegistration<T>
        where T : class
    {
        public void Register(T container)
        {
            var types = GetTypesWithAttributes<TypeRegistrationAttribute>();

            var DerivedTypes = GetAllDerivedTypes(types);

            registerAllTypes(container, DerivedTypes);
        }

        private void registerAllTypes(T container, Dictionary<TypeAndAttributeData<TypeRegistrationAttribute>, List<TypeAndAttributeData<DerivedTypeRegistrationAttribute>>> derivedTypes)
        {
            foreach (var baseType in derivedTypes.Keys)
            {
                foreach (var derivedType in derivedTypes[baseType])
                {
                    var lifetimeManagement = LifetimeManagementType.Default;

                    if (derivedType.AttributeType != null)
                    {
                        lifetimeManagement = derivedType.AttributeType.lifetimeManagementType;
                    }

                    if (baseType.TypeForRegistration != derivedType.TypeForRegistration)
                    {
                        if (derivedTypes[baseType].Count>1)
                            registerManyInContainer(container, baseType.TypeForRegistration, derivedType.TypeForRegistration, lifetimeManagement);
                        else
                            registerInContainer(container, baseType.TypeForRegistration, derivedType.TypeForRegistration, lifetimeManagement);
                    }
                }
            }
        }

        protected abstract void registerInContainer(T container, Type baseType, Type derivedType, LifetimeManagementType lifetimeManagement);

        protected abstract void registerManyInContainer(T container, Type baseType, Type derivedType, LifetimeManagementType lifetimeManagement);

        private List<TypeAndAttributeData<T>> GetTypesWithAttributes<T>()
            where T : Attribute
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            return currentAssembly.GetTypes()
                .Where(item => item.GetCustomAttributes(false).Any(item2 => item2 is T))
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

        private List<TypeAndAttributeData<T>> GetDerivedTypes<T>(Type type)
            where T : Attribute
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            return currentAssembly.GetTypes()
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
        private Dictionary<TypeAndAttributeData<TypeRegistrationAttribute>, List<TypeAndAttributeData<DerivedTypeRegistrationAttribute>>> GetAllDerivedTypes(List<TypeAndAttributeData<TypeRegistrationAttribute>> baseTypes)
        {
            var res = new Dictionary<TypeAndAttributeData<TypeRegistrationAttribute>, List<TypeAndAttributeData<DerivedTypeRegistrationAttribute>>>();

            foreach (var type in baseTypes)
            {
                var derivedTypes = GetDerivedTypes<DerivedTypeRegistrationAttribute>(type.TypeForRegistration);

                res.Add(type, derivedTypes);
            }

            return res;
        }
    }
}

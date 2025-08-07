using RegistrationByAttributes.Attributes;

namespace test.Tests.Simple.TestOneToOne
{
    [TypeRegistrationAttribute(LifetimeManagementType.Singletone)]
    internal interface IOneToOneGenericHardOverride<T>
    {
    }
}

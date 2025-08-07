using RegistrationByAttributes.Attributes;

namespace test.Tests.Simple.TestOneToOne
{
    [TypeRegistrationAttribute(LifetimeManagementType.Default)]
    internal interface IOneToManyGenericHard<T>
    {
    }
}

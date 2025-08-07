using RegistrationByAttributes.Attributes;

namespace test.Tests.Simple.TestOneToOne
{
    [DerivedTypeRegistration(LifetimeManagementType.Singletone)]
    internal class OneToManyTwoGenericHardOverride<T> : IOneToManyGenericHardOverride<T>
    {
    }
}

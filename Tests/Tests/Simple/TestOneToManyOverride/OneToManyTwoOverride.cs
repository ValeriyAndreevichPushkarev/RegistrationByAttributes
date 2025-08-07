using RegistrationByAttributes.Attributes;

namespace test.Tests.Simple.TestOneToOne
{
    [DerivedTypeRegistration(LifetimeManagementType.Singletone)]
    internal class OneToManyTwoOverride : IOneToManyOverride
    {
    }
}

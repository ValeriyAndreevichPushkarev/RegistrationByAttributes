using RegistrationByAttributes.Attributes;

namespace test.Tests.Simple.TestOneToOne
{
    [DerivedTypeRegistration(LifetimeManagementType.Singletone)]
    internal class OneToManyTwoGenericOverride : IOneToManyGenericOverride<int>
    {
    }
}

using RegistrationByAttributes.Attributes;

namespace test.Tests.Simple.TestOneToOne
{
    [DerivedTypeRegistration(LifetimeManagementType.PerResolve)]
    internal class OneToManyOneGenericOverride : IOneToManyGenericOverride<int>
    {
    }
}

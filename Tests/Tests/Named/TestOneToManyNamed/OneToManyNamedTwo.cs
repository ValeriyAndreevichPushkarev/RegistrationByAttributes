using RegistrationByAttributes.Attributes;

namespace test.Tests.Simple.TestOneToOne
{
    [DerivedTypeRegistration(LifetimeManagementType.PerResolve, "Two")]
    internal class OneToManyNamedTwo : IOneToManyNamed
    {
    }
}

using RegistrationByAttributes.Attributes;

namespace Tests.Tests.GenericHard.Mixed
{
    [DerivedTypeRegistration(LifetimeManagementType.Singletone)]
    internal class GenericHardMixedImpl : IGenericHardMixed<int>
    {
    }
}

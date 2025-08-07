namespace RegistrationByAttributes.Attributes
{
    /// <summary>
    /// Attribute for changing LifetimeManagement in derived types
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DerivedTypeRegistrationAttribute : Attribute
    {
        public DerivedTypeRegistrationAttribute(LifetimeManagementType lifetimeManagement)
        {
            lifetimeManagementType = lifetimeManagement;
        }

        public LifetimeManagementType lifetimeManagementType;
    }
}

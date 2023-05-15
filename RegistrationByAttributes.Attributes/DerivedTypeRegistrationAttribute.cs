namespace RegistrationByAttributes.Attributes
{
    /// <summary>
    /// Attribute for changing LifetimeManagement in derived types
    /// </summary>
    public class DerivedTypeRegistrationAttribute : Attribute
    {
        public DerivedTypeRegistrationAttribute(LifetimeManagementType lifetimeManagement)
        {
            lifetimeManagementType = lifetimeManagement;
        }

        public LifetimeManagementType lifetimeManagementType;
    }
}

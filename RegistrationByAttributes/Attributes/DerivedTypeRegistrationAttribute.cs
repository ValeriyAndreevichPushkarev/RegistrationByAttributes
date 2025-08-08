namespace RegistrationByAttributes.Attributes
{
    /// <summary>
    /// Attribute for changing LifetimeManagement in derived types
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DerivedTypeRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Attribute that redefines container configuration
        /// </summary>
        /// <param name="lifetimeManagement">Lifetime management for specified class</param>
        /// <param name="name">Name for named instance, or null</param>
        public DerivedTypeRegistrationAttribute(LifetimeManagementType lifetimeManagement = LifetimeManagementType.Default, string name = null)
        {
            lifetimeManagementType = lifetimeManagement;
            this.name = name;
        }

        public LifetimeManagementType lifetimeManagementType;

        public string name;
    }
}

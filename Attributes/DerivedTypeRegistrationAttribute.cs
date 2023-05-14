namespace RegistrationByAttributes.Attributes
{
    /// <summary>
    /// Атрибут для изменения LifetimeManagementType у производных типов
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

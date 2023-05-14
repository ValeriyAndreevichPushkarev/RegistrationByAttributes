namespace RegistrationByAttributes.Attributes
{
    /// <summary>
    /// Атрибут для базовых типов
    /// </summary>
    public class TypeRegistrationAttribute : Attribute
    {
        public TypeRegistrationAttribute(LifetimeManagementType lifetimeManagement)
        {
            lifetimeManagementType = lifetimeManagement;
        }

        public LifetimeManagementType lifetimeManagementType;
    }
}

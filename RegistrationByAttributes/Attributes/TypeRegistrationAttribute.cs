using System;

namespace RegistrationByAttributes.Attributes
{
    /// <summary>
    /// Attribute for base types
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class TypeRegistrationAttribute : Attribute
    {
        public TypeRegistrationAttribute(LifetimeManagementType lifetimeManagement)
        {
            lifetimeManagementType = lifetimeManagement;
        }

        public LifetimeManagementType lifetimeManagementType;
    }
}

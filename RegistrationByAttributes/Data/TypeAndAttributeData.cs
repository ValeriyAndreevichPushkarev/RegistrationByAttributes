namespace RegistrationByAttributes.Data
{
    /// <summary>
    /// Information about type for registration and attribute
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TypeAndAttributeData<T>
        where T : Attribute
    {
        /// <summary>
        /// Type for registration
        /// </summary>
        public Type TypeForRegistration;

        /// <summary>
        /// Registration attribute
        /// </summary>
        public T AttributeType;
    }
}

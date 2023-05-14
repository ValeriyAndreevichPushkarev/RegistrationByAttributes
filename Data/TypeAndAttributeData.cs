namespace RegistrationByAttributes.Data
{
    /// <summary>
    /// Сведения о типе и соответствующем атрибуте
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TypeAndAttributeData<T>
        where T : Attribute
    {
        /// <summary>
        /// Тип для регистрации
        /// </summary>
        public Type TypeForRegistration;

        /// <summary>
        /// Обьект атрибута
        /// </summary>
        public T AttributeType;
    }
}

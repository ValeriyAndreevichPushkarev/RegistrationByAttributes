namespace RegistrationByAttributes.Attributes
{
    /// <summary>
    /// Типы времени жизни обьекта
    /// </summary>
    public enum LifetimeManagementType
    {
        /// <summary>
        /// Синглтон
        /// </summary>
        Singletone,
        /// <summary>
        /// 1 на поток
        /// </summary>
        PerThread,
        /// <summary>
        /// Создается каждый раз
        /// </summary>
        PerResolve,
        /// <summary>
        /// По умолчанию
        /// </summary>
        Default
    }
}

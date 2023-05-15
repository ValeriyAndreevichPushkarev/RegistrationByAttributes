namespace RegistrationByAttributes.Attributes
{
    /// <summary>
    /// LifeTime of object in container
    /// </summary>
    public enum LifetimeManagementType
    {
        /// <summary>
        /// Singletone
        /// </summary>
        Singletone,
        /// <summary>
        /// One per Thread
        /// </summary>
        PerThread,
        /// <summary>
        /// Creates every time
        /// </summary>
        PerResolve,
        /// <summary>
        /// Default
        /// </summary>
        Default
    }
}

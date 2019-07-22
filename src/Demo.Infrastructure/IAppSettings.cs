namespace Demo.Infrastructure
{
    using System;

    /// <summary>
    /// The IAppSettings interface.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Gets the secret key.
        /// </summary>
        /// <value>
        /// The secret key.
        /// </value>
        string Secret { get; }
    }
}

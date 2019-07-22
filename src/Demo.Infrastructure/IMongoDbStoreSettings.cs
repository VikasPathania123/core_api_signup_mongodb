namespace Demo.Infrastructure
{
    using System;

    /// <summary>
    /// The MongoStoreSettings interface.
    /// </summary>
    public interface IMongoDbStoreSettings
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        /// <value>
        /// The database name.
        /// </value>
        string DbName { get; }

        /// <summary>
        /// Gets the max connection idle time.
        /// </summary>
        /// <value>
        /// The max connection idle time.
        /// </value>
        double MaxConnectionIdleTime { get; }


        /// <summary>
        /// Gets the socket timeout.
        /// </summary>
        /// <value>
        /// The socket timeout.
        /// </value>
        double SocketTimeout { get; }
    }
}

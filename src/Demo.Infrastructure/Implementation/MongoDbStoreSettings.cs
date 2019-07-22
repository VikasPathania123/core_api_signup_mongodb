namespace Demo.Infrastructure.Implementation
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The mongo store settings.
    /// </summary>
    public class MongoDbStoreSettings : IMongoDbStoreSettings
    {

        private readonly IConfiguration config;

        public MongoDbStoreSettings(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        private string connectionString = null;
        public string ConnectionString
        {
            get
            {
                this.connectionString = string.Format(this.config["MongoDb:ConnectionString"]);
                return this.connectionString;
            }
        }

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        /// <value>
        /// The database name.
        /// </value>
        private string dbName = null;
        public string DbName
        {
            get
            {
                this.dbName = string.Format(this.config["MongoDb:DbName"]);
                return this.dbName;
            }
        }

        /// <summary>
        /// Gets or sets the max connection idle time.
        /// </summary>
        /// <value>
        /// The max connection idle time.
        /// </value>
        public double MaxConnectionIdleTime
        {
            get
            {
                var minutes = double.Parse(this.config["MongoDb:MaxConnectionIdleTime"]);
                return minutes == 0 ? 1 : minutes;
            }
        }

        /// <summary>
        /// Gets or sets the socket timeout.
        /// </summary>
        /// <value>
        /// The socket timeout.
        /// </value>
        public double SocketTimeout
        {
            get
            {
                var minutes = double.Parse(this.config["MongoDb:SocketTimeout"]);
                return minutes == 0 ? 1 : minutes;
            }
        }
    }
}

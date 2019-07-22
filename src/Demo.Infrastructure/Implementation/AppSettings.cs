namespace Demo.Infrastructure.Implementation
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The mongo store settings.
    /// </summary>
    public class AppSettings : IAppSettings
    {

        private readonly IConfiguration config;

        public AppSettings(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        /// <value>
        /// The secret key.
        /// </value>
        private string secret = null;
        public string Secret
        {
            get
            {
                this.secret = string.Format(this.config["AppSettings:Secret"]);
                return this.secret;
            }
        }
    }
}

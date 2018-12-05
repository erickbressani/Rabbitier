using Microsoft.Extensions.Configuration;

namespace Rabbitier.Configuration
{
    internal class RabbitierConfiguration
    {
        public string HostName { get; private set; } = "localhost";
        public string UserName { get; private set; } = "guest";
        public string Password { get; private set; } = "guest";
        public string VirtualHost { get; private set; }
        public int Port { get; private set; }

        public RabbitierConfiguration()
        {
            IConfigurationRoot config = GetConfig();
            SetConfig(config);
        }

        private IConfigurationRoot GetConfig()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                             .Build();
        }

        private void SetConfig(IConfigurationRoot config)
        {
            HostName = config["Rabbitier:HostName"];
            UserName = config["Rabbitier:UserName"];
            Password = config["Rabbitier:Password"];
            VirtualHost = config["Rabbitier:VirtualHost"];

            string port = config["Rabbitier:Port"];

            if (!string.IsNullOrEmpty(port))
            {
                int.TryParse(port, out int parsedPort);
                Port = parsedPort;
            }
        }
    }
}

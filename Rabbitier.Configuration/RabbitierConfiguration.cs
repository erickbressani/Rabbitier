using Microsoft.Extensions.Configuration;

namespace Rabbitier.Configuration
{
    internal class RabbitierConfiguration
    {
        public string HostName { get; } = "localhost";
        public string UserName { get; } = "guest";
        public string Password { get; } = "guest";
        public string VirtualHost { get; }
        public int Port { get; }

        public RabbitierConfiguration()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                   .Build();

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

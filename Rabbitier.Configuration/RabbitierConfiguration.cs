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

        }
    }
}

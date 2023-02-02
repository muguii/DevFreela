using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;

        // Caso o servidor do RabbitMQ seja externo, precisa configurar informações de endereço e usuario. Portanto, geralmente se usa o IConfiguration para passar tais informações.
        //public MessageBusService(IConfiguration configuration)
        //{
        //    _factory = new ConnectionFactory { HostName = "server", UserName = "username", Password = "password" };
        //}

        public MessageBusService()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
        }

        public void Publish(string queueName, byte[] messages)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Garantir que a fila esteja criada
                    channel.QueueDeclare(queue: queueName, 
                                         durable: false, 
                                         exclusive: false, 
                                         autoDelete: false, 
                                         arguments: null);

                    channel.BasicPublish(exchange: "", // Default
                                         routingKey: queueName, 
                                         basicProperties: null, 
                                         body: messages);
                }
            }
        }
    }
}

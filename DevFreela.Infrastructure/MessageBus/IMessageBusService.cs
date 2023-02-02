namespace DevFreela.Infrastructure.MessageBus
{
    public interface IMessageBusService
    {
        void Publish(string queueName, byte[] messages);
    }
}

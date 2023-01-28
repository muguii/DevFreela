namespace DevFreela.Core.Services
{
    public interface IMessageBusService
    {
        void Publish(string queueName, byte[] messages);
    }
}

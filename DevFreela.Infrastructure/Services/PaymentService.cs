using DevFreela.Infrastructure.MessageBus;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Services
{
    public class PaymentInfoDTO
    {
        public int IdProject { get; private set; }
        public string CreditCardNumber { get; private set; }
        public string Cvv { get; private set; }
        public string ExpiresAt { get; private set; }
        public string FullName { get; private set; }
        public decimal Amount { get; private set; }

        public PaymentInfoDTO(int idProject, string creditCardNumber, string cvv, string expiresAt, string fullName, decimal amount)
        {
            IdProject = idProject;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
            Amount = amount;
        }
    }

    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private const string PAYMENT_PROCESS_QUEUE = "Payments";

        public PaymentService(IMessageBusService messageBusService)
        {
            this._messageBusService = messageBusService;
        }

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);
            _messageBusService.Publish(PAYMENT_PROCESS_QUEUE, paymentInfoBytes);
        }
    }
}

namespace DevFreela.Core.IntegrationEvents
{
    public class PaymentApprovedIntegrationEvents
    {
        public int IdProject { get; set; }

        public PaymentApprovedIntegrationEvents(int idProject)
        {
            IdProject = idProject;
        }
    }
}

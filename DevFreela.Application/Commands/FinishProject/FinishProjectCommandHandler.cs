using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            this._unitOfWork = unitOfWork;
            this._paymentService = paymentService;
        }


        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(request.Id);

            var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project.TotalCost);

            _paymentService.ProcessPayment(paymentInfoDto);

            project.SetPaymentPending();

            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}

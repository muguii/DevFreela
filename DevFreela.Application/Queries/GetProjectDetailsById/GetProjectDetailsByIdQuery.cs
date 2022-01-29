using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectDetailsById
{
    public class GetProjectDetailsByIdQuery : IRequest<ProjectDetailsViewModel>
    {
        public int Id { get; private set; }

        public GetProjectDetailsByIdQuery(int id)
        {
            Id = id;
        }
    }
}

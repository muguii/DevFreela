using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<Project>
    {
        public int Id { get; private set; }

        public GetProjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}

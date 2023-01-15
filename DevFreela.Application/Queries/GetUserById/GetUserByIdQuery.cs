using MediatR;

namespace DevFreela.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}

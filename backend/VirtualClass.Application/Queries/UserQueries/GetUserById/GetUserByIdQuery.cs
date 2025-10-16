using MediatR;
using VirtualClass.Application.ViewModel;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdQuery : IRequest<ServiceResult<UserViewModel>>
    {
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

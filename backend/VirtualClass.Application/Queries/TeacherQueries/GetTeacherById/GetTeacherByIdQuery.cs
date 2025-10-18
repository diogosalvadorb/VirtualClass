using MediatR;
using VirtualClass.Application.ViewModel.TeacherViewModels;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.TeacherQueries.GetTeacherById
{
    public class GetTeacherByIdQuery : IRequest<ServiceResult<TeacherViewModel>>
    {
        public GetTeacherByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

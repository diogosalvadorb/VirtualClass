using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualClass.Application.ViewModel;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ServiceResult<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<ServiceResult<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.Id);

                if (user == null)
                {
                    return ServiceResult<UserViewModel>.Error("Usuário não encontrado.", ErrorTypeEnum.NotFound);
                }

                return ServiceResult<UserViewModel>.Success(
                    new UserViewModel(user.Id, user.FullName, user.Email));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserViewModel>.Error($"Erro ao buscar usuário: {ex.Message}", ErrorTypeEnum.Failure);
            }
        }
    }
}

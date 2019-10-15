using MediatR;
using score.Dto;
using score.Infrastructure;
using score.Services.SecurityService;
using score.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace score.Domain.Autentication.Login
{
    /// <summary>
    /// This class handles the procces for users loggin and registers tokens in database
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, CommandResult>
    {

        private readonly ISecurityService securityService;
        private readonly IUserService userService;

        public LoginCommandHandler(ISecurityService securityService, IUserService userService)
        {
            this.securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<CommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userDto = userService.getUserByEmail(request.email); 
                if(userDto is null)
                {
                    return CommandResult.Failure("Invalid Email");
                }

                if (!securityService.VerifiedPassword(request.password, userDto.Password, userDto.Salt))
                {
                    return CommandResult.Failure("Invalid Password");
                }

                var token = securityService.getToken(userDto);
                var userToken = new UserTokensDto
                {
                    Id = 0,
                    IdUser = userDto.Id,
                    token = Encoding.UTF8.GetBytes(token)
                };

                userService.SaveUserToken(userToken);

                if (string.IsNullOrWhiteSpace(token))
                {
                    return CommandResult.Failure("Token Not Generated");
                }

                return CommandResult.Success(token);
            }
            catch (Exception ex)
            {
                return CommandResult.Failure(ex);
            }
        }
    }
}

using score.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace score.Domain.Autentication.Login
{
    /// <summary>
    /// Class with the necessary fields for user validation
    /// </summary>
    public class LoginCommand : IRequest<CommandResult>
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}

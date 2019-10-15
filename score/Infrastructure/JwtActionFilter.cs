using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using score.Services.RequestData;
using score.Services.SecurityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Infrastructure
{
    /// <summary>
    /// This class detects token in the header request and verifies the token and the user 
    /// </summary>
    public class JwtActionFilter : IActionFilter
    {
        private readonly ISecurityService security;
        private readonly IRequestData requestData; 

        public JwtActionFilter(ISecurityService security, IRequestData requestData)
        {
            this.security = security ?? throw new ArgumentNullException(nameof(security));
            this.requestData = requestData ?? throw new ArgumentException(nameof(requestData));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var hasToken = context.HttpContext.Request.Headers.TryGetValue("Token", out var token);
            if (!hasToken)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!security.ValidToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            security.getClaim(token, "Id", out var IdUser);
            security.getClaim(token, "Name", out var UserName);
            requestData.setData("Id", IdUser);
            requestData.setData("Name", UserName);
            
        }
    }
}

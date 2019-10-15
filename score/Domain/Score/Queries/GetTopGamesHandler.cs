using MediatR;
using score.Infrastructure;
using score.Services.RequestData;
using score.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace score.Domain.Score.Queries
{

    /// <summary>
    /// Class that processes the list of the top rated games for the logged users
    /// </summary>
    public class GetTopGamesHandler : IRequestHandler<GetTopGamesQuery, QueryResult>
    {

        private readonly IUserService userService;
        private readonly IRequestData requestData;

        public GetTopGamesHandler(IUserService userService, IRequestData requestData)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.requestData = requestData ?? throw new ArgumentNullException(nameof(requestData));
        }

        public async Task<QueryResult> Handle(GetTopGamesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.NumRows <= 0)
                {
                    return QueryResult.Failure("Invalid Number of Rows");
                }

                if (requestData.exists("Id"))
                {
                    var Id = int.Parse(requestData.getData("Id"));
                    var lista = userService.getTopGames(Id, request.NumRows);
                    return QueryResult.Success(lista);
                }
                else
                {
                    return QueryResult.Success();
                }
                                             
            }
            catch (Exception ex)
            {
                return QueryResult.Failure(ex);
            }
        }
    }
}

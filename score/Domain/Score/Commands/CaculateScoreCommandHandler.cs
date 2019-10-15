using MediatR;
using score.Dto;
using score.Infrastructure;
using score.Model;
using score.Services.RequestData;
using score.Services.UserService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace score.Domain.Score.Commands
{
    /// <summary>
    /// This class creates the object that calculate the escore for the bowling game 
    /// and registers the result in databse (only for logged users)
    /// </summary>
    public class CaculateScoreCommandHandler : IRequestHandler<CalculateScoreCommand, CommandResult>
    {

        private readonly IRequestData requestData;
        private readonly IUserService userService;

        public CaculateScoreCommandHandler(IRequestData requestData, IUserService userService)
        {
            this.requestData = requestData ?? throw new ArgumentNullException(nameof(requestData));
            this.userService = userService ?? throw new ArgumentNullException(nameof(requestData));
        }

        public async Task<CommandResult> Handle(CalculateScoreCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var rollSequence = new RollSequence(request.RollsSequence);
                var finalScore = rollSequence.getFinalScore();
                Log.Logger.Information($"Final Score : {finalScore}");

                if (requestData.exists("Id"))
                {
                    var idUser = int.Parse(requestData.getData("Id"));
                    var userGame = new UserGameDto
                    {
                        Id = 0,
                        IdUser = idUser,
                        Sequence = request.RollsSequence,
                        FinalScore = finalScore
                    };

                    userService.SaveUserGame(userGame);
                }

                return CommandResult.Success(finalScore);
            }
            catch (Exception ex)
            {                
                return CommandResult.Failure(ex);
            }

            
           
        }
    }
}

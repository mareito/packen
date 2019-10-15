using score.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Services.UserService
{
    /// <summary>
    /// Interface that defines methods for user service
    /// </summary>
    public interface IUserService
    {      
        UserDto getUserByEmail(string email);
        void SaveUser(UserDto user);
        void SaveUserToken(UserTokensDto userToken);
        UserDto getUserById(int id);
        List<UserTokensDto> getTokensUser(int IdUser);
        void SaveUserGame(UserGameDto userGameDto);
        IEnumerable<UserGameDto> getTopGames(int UserId, int numRows);
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using score.Data;
using score.Data.Entities;
using score.Dto;
using score.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Services.UserService
{
        
    public class UserService : IUserService
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;

        public UserService(ApplicationContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(context));
        }

        public UserDto getUserByEmail(string email)
        {
            var userEmail = (from user in context.users
                             where user.Email == email
                             select user).AsNoTracking().SingleOrDefault();

            return userEmail is null ? null : mapper.Map<UserDto>(userEmail);
        }

        public UserDto getUserById(int id)
        {
            var userId = (from user in context.users
                             where user.Id == id
                             select user).AsNoTracking().SingleOrDefault();

            return userId is null ? null : mapper.Map<UserDto>(userId);
        }

        public void SaveUser(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            context.users.Add(user);
            context.SaveChanges();                     
        }

        public void SaveUserGame(UserGameDto userGameDto)
        {
            var userGame = mapper.Map<UserGame>(userGameDto);
            context.userGames.Add(userGame);
            context.SaveChanges();
        }

        public void SaveUserToken(UserTokensDto userTokenDto)
        {
            context.UserTokens.Add(mapper.Map<UserTokens>(userTokenDto));
            context.SaveChanges();           
        }

        public List<UserTokensDto> getTokensUser(int IdUser)
        {
            var tokens = (from tokUs in context.UserTokens
                          where tokUs.IdUser == IdUser
                          select tokUs).AsNoTracking().ToList();

            List<UserTokensDto> tokenList = new List<UserTokensDto>();
            foreach (var token in tokens)
            {
                tokenList.Add(mapper.Map <UserTokensDto>(token));
            }

            return tokenList;
        }

        public IEnumerable<UserGameDto> getTopGames(int UserId, int numRows)
        {
            var games = (from usGame in context.userGames
                         where usGame.IdUser == UserId
                         select usGame)
                         .OrderByDescending(c => c.FinalScore)
                         .Take(numRows)
                         .AsNoTracking()
                         .ToList();

            var gameList = new List<UserGameDto>();
            foreach (var game in games)
            {
                gameList.Add(mapper.Map<UserGameDto>(game));
            }

            return gameList;
        }
    }
}

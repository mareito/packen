using AutoMapper;
using score.Data.Entities;
using score.Dto;
using score.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Configuration.AutoMapper
{
    /// <summary>
    /// Default profile for AutoMapper
    /// </summary>
    public class MappingProfile : Profile
    {     
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserTokens, UserTokensDto>().ReverseMap();
            CreateMap<UserGame, UserGameDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebGame.Data;

namespace WebGame.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //base.CreateMap<Game, GameDTO>()
            //    .ForMember(dto => dto.Platforms, cfg => cfg.MapFrom(game => game.Platforms.Select(platform => platform.Name)))
            //    .ForMember(dto => dto.Genres, cfg => cfg.MapFrom(game => game.Genres.Select(genre => genre.Name)));

            //base.CreateMap<GameDTO, Game>()
            //    .ForMember(game => game.Platforms, cfg => cfg.MapFrom(dto => new List<Platform>()))
            //    .ForMember(game => game.Genres, cfg => cfg.MapFrom(dto => new List<Genre>()));

            //base.CreateMap<CommentDTO, Comment>();
            //-----------------------------------------------------

            CreateMap<Game, GameDto>()
                    .ForMember(dto => dto.PlatformTypes,
                               cfg => cfg.MapFrom(game => game.PlatformTypes.Select(platform => platform.Type)))
                    .ForMember(dto => dto.Genres,
                               cfg => cfg.MapFrom(game => game.Genres.Select(genre => genre.Name)));

            CreateMap<GameDto, Game>()
                .ForMember(game => game.PlatformTypes, cfg => cfg.MapFrom(dto => new List<PlatformType>()))
                .ForMember(game => game.Genres, cfg => cfg.MapFrom(dto => new List<Genre>()))
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>()
                .ForMember(m => m.Id, opt => opt.Ignore()); 
        }
    }
}
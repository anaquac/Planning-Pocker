using AutoMapper;
using Planning_Poker.Dto;
using Planning_Poker.Models;

namespace Planning_Poker.MapperProfiles
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<User, UserDto>().ForMember(dest => 
                dest.Name, opt => 
                opt.MapFrom(src => src.Name))
                .ForMember(dest=> dest.Id, opt=>
                    opt.MapFrom(src=>src.Id));
            CreateMap<Letters, LetterDto>().ForMember(dest =>
                dest.value, opt =>
                opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id));
            CreateMap<UserStory, UserStoryDto>().ForMember(dest =>
                dest.Description, opt =>
                opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id));
            CreateMap<Votes, VotesDto>().ForMember(dest =>
                dest.user, opt =>
                opt.MapFrom(src => src.User))
                .ForMember(dest =>
                    dest.user, opt => 
                    opt.MapFrom(src => src.Letters))
                    .ForMember(dest =>
                    dest.storyDto, opt =>
                    opt.MapFrom(src => src.UserStory))
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id));
        }
    }
}

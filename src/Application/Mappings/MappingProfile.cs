using Application.DTO;
using Application.Requests.UserRequest;
using Application.Requests.UserRequests;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Developer, DeveloperDTO>().ReverseMap();
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<RegistrationRequest, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}

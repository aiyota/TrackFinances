using AutoMapper;
using TrackFinances.Api.Contracts.V1;
using TrackFinances.Api.Contracts.V1.Requests;
using TrackFinances.Api.Contracts.V1.Responses;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateRequest, UserCreate>();
        CreateMap<UserUpdateRequest, UserUpdate>();
        CreateMap<User, UserResponse>();
    }
}

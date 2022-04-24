using AutoMapper;
using TrackFinances.Api.Contracts.V1;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateRequest, UserCreate>();
        CreateMap<UserUpdateRequest, UserUpdate>();
    }
}

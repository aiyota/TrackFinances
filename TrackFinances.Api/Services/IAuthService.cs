namespace TrackFinances.Api.Services;

public interface IAuthService
{
    string CreateToken(Guid userId);
}

namespace TrackFinances.Api.Contracts.V1.Requests;

public class UserLoginRequest
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

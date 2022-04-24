namespace TrackFinances.Api.Contracts.V1.Requests;

public class UserCreateRequest
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

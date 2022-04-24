namespace TrackFinances.Api.Contracts.V1;

public class UserUpdateRequest
{
    public string Id { get; set; } = default!;
    public string? UserName { get; set; } = default;
    public string? Email { get; set; } = default;
    public string? PasswordHash { get; set; } = default;
}

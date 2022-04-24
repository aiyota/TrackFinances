namespace TrackFinances.Api.Contracts.V1.Responses;

public class UserResponse
{
    public Guid Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool EmailConfirmed { get; set; } = default!;
    public DateTime? DateJoined { get; set; }
}

using TrackFinances.Api.Contracts;
using TrackFinances.Api.Contracts.V1;
using TrackFinances.Api.Contracts.V1.Requests;
using TrackFinances.Api.Services;
using TrackFinances.DataAccess.Data;

namespace TrackFinances.Api.Endpoints;

public static class UserEndpoints
{
    public static void UseUserEndpoints(this WebApplication app)
    {
        app.MapGet(ApiRoutes.User.Get, Get);
        app.MapGet(ApiRoutes.User.GetBy, GetBy);
        app.MapPost(ApiRoutes.User.Create, Create);
    }

    private async static Task<IResult> Create(UserCreateRequest userCreateRequest, IUserService userService)
    {
        var createdUser = await userService.CreateAsync(userCreateRequest);
        if (createdUser is null)
            return Results.BadRequest();

        var uri = ApiRoutes.User.Create + "/" + createdUser.Id;
        return Results.Created(uri, createdUser);
    }

    private async static Task<IResult> Get(string userId, IUserService userService)
    {
        var user = await userService.GetAsync(userId);
        return (user is not null) 
                ? Results.Ok(user)
                : Results.NotFound();
    }

    private async static Task<IResult> GetBy(string? email, string? userName, IUserService userService)
    {
        if (email is not null && userName is not null)
            return Results.BadRequest(new { 
                Message = "Pass either email or userName, not both." 
            });

        if (email is not null)
        {
            var user = await userService.GetByEmailAsync(email);
            return (user is not null)
                    ? Results.Ok(user)
                    : Results.NotFound();
        }

        if (userName is not null)
        {
            var user = await userService.GetByUserNameAsync(userName);
            return (user is not null)
                    ? Results.Ok(user)
                    : Results.NotFound();
        }

        return Results.BadRequest();
    }
}

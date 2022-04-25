using AutoMapper;
using TrackFinances.Api.Contracts;
using TrackFinances.Api.Contracts.V1.Requests;
using TrackFinances.Api.Contracts.V1.Responses;
using TrackFinances.Api.Services;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.Endpoints;

public static class UserEndpoints
{
    public static void UseUserEndpoints(this WebApplication app)
    {
        app.MapGet(ApiRoutes.User.Get, Get);
        app.MapGet(ApiRoutes.User.GetBy, GetBy);
        app.MapPost(ApiRoutes.User.Create, Create);
        app.MapPost(ApiRoutes.User.Login, Login);
    }

    private async static Task<IResult> Create(UserCreateRequest userCreateRequest,
                                              IUserService userService,
                                              HttpContext context,
                                              IMapper mapper)
    {
        var mappedUser = mapper.Map<UserCreate>(userCreateRequest);
        var createdUser = await userService.CreateAsync(mappedUser, userCreateRequest.Password);
        if (createdUser is null)
            return Results.BadRequest();

        var uri = ApiRoutes.CreateUri(context,
                                      ApiRoutes.User.Create,
                                      createdUser.Id.ToString());

        var userResponse = mapper.Map<UserResponse>(createdUser);
        return Results.Created(uri, userResponse);
    }

    private async static Task<IResult> Get(string userId,
                                           IUserService userService,
                                           IMapper mapper)
    {
        var user = await userService.GetAsync(userId);
        var userResponse = mapper.Map<UserResponse>(user);
        return MakeNullableResult(userResponse);
    }

    private async static Task<IResult> GetBy(string? email,
                                             string? userName,
                                             IUserService userService,
                                             IMapper mapper)
    {
        if (email is not null && userName is not null)
            return Results.BadRequest(new { 
                Message = "Pass either email or userName, not both." 
            });

        if (email is not null)
        {
            var user = await userService.GetByEmailAsync(email);
            var userResponse = mapper.Map<UserResponse>(user);
            return MakeNullableResult(userResponse);
        }

        if (userName is not null)
        {
            var user = await userService.GetByUserNameAsync(userName);
            var userResponse = mapper.Map<UserResponse>(user);
            return MakeNullableResult(userResponse);
        }

        return Results.BadRequest();
    }

    private async static Task<IResult> Login(UserLoginRequest request,
                                             IUserService userService)
    {
        var loginSuccess = await userService.Login(request.UserName, request.Password);
        return (loginSuccess) 
                ? Results.Ok() 
                : Results.Unauthorized();
    }

    private static IResult MakeNullableResult<T>(T? input = null)
    where T : class =>
        (input is not null)
            ? Results.Ok(input)
            : Results.NotFound();
}

using TrackFinances.Api.Endpoints;
using TrackFinances.Api.Services;
using TrackFinances.DataAccess.Data;
using TrackFinances.DataAccess.DbAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddSingleton<ICategoryData, CategoryData>();
builder.Services.AddSingleton<IExpenseData, ExpenseData>();

builder.Services.AddSingleton<IHashingService, HashingService>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseUserEndpoints();

app.Run();

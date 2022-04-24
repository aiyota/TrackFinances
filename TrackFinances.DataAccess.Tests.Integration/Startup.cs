using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.Data;
using TrackFinances.DataAccess.DbAccess;

namespace TrackFinances.DataAccess.Tests.Integration;

 public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        IConfiguration config = new ConfigurationBuilder()
                                   .AddJsonFile("appsettings.json")
                                   .Build();
        services.AddSingleton(config);
        services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        services.AddSingleton<IUserData, UserData>();
        services.AddSingleton<ICategoryData, CategoryData>();
        services.AddSingleton<IExpenseData, ExpenseData>();
    }
}

using Data;
using Data.Contracts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using WebFramework.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var SqlConnectionProvider = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddDbContext<AppDBContext>(option => {
    option.UseSqlServer(SqlConnectionProvider,
        a => { 
            a.CommandTimeout(60);
            a.MigrationsAssembly(typeof(AppDBContext).Assembly.ToString());
        });
});

builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));



var app = builder.Build();

app.ExceptionHadlerMiddle();

app.UseHttpsRedirection();

app.Run();
using Common;
using Data;
using Data.Contracts;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Services;
using WebFramework.Configuration;
using WebFramework.Middlewares;


#region Services

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

var SqlConnectionProvider = builder.Configuration.GetConnectionString("SqlServer");

var sitesetting = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

//Inject SiteSetting class to IServiceContainer
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

builder.Services.AddDbContext<AppDBContext>(option => {
    option.UseSqlServer(SqlConnectionProvider,
        a => { 
            a.CommandTimeout(60);
            a.MigrationsAssembly(typeof(AppDBContext).Assembly.ToString());
        });
});

builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));
builder.Services.AddScoped<IJwtService,JwtService>();
builder.Services.AddScoped<IUserRepo,UserRepo>();
builder.Services.AddJwtAuthentication(sitesetting.JwtSettings);

    builder.Services.AddMvc(options => {
        options.Filters.Add(new AuthorizeFilter());//Added Authorize to all project
    });
#endregion


#region Middlewares

var app = builder.Build();

app.ExceptionHadlerMiddle();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMvc();

app.Run();
#endregion
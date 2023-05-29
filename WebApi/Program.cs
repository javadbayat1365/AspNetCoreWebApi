using Autofac.Extensions.DependencyInjection;
using Common;
using Data;
using Data.Contracts;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.ControlServices;
using WebFramework.Configuration;
using WebFramework.Middlewares;


#region Services


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var sitesetting = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

//Inject SiteSetting class to IServiceContainer
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddIdentityConfiguration();

builder.Services.AddJwtAuthentication(sitesetting.JwtSettings);


var services = builder.Services.BuildAutofacServiceProvider();

//Declare RequiredHttps Attribute globaly   
if (builder.Environment.IsProduction())
{
    builder.Services.Configure<MvcOptions>(o => o.Filters.Add(new RequireHttpsAttribute()));
}
#endregion


#region Middlewares

var app = builder.Build();

app.ExceptionHadlerMiddle();

if (app.Environment.IsProduction())
{
    app.UseHsts(); //Http Strict Transport Security => add https securit to header request

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
#pragma warning disable MVC1005 // Cannot use UseMvc with Endpoint Routing
app.UseMvc();
#pragma warning restore MVC1005 // Cannot use UseMvc with Endpoint Routing

app.Run();
#endregion
using ShopTARge22.Data;
using Microsoft.EntityFrameworkCore;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.ApplicationServices.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.SignalR;
using ShopTARge22.Hubs;
using Microsoft.AspNetCore.Identity;
using ShopTARge22.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IRealEstatesServices, RealEstatesServices>();
builder.Services.AddScoped<IKindergartensServices, KindergartensServices>();
builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();
builder.Services.AddScoped<IChuckNorrisJokesServices, ChuckNorrisJokesServices>();
builder.Services.AddScoped<ICoctailServices, CoctailServices>();
builder.Services.AddScoped<IAccuWeatherServices, AccuWeatherServices>();
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddSignalR();

builder.Services.AddDbContext<ShopTARge22Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ShopTARge22Context>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

//builder.Services.AddDefaultIdentity<IdentityUser>()
//	.AddEntityFrameworkStores<ShopTARge22Context>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider
    (Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
    RequestPath = "/multipleFileUpload"
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");

app.Run();

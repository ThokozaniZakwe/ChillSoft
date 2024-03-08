using Chillisoft.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

app.UseStaticFiles();
app.UseStatusCodePages();

app.MapDefaultControllerRoute();
//app.MapGet("/", () => "Hello World!");

app.Run();

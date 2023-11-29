using Microsoft.EntityFrameworkCore;
using Neatburguer.Models.Entities;
using Neatburguer.Repositories;
using NuGet.Protocol.Core.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

string conexion = "server=localhost;user=root;password=root;database=neat";

builder.Services.AddDbContext<NeatContext>(optionBuilder => optionBuilder.UseMySql(conexion,ServerVersion.AutoDetect(conexion)));
builder.Services.AddTransient<Repository<Menu>>();
builder.Services.AddTransient<MenuRepository>();
builder.Services.AddTransient<Repository<Clasificacion>>();


var app = builder.Build();


app.UseStaticFiles();
app.UseFileServer();
app.MapDefaultControllerRoute();
app.Run();

//app.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );

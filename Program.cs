using IronGrip.Data;
using IronGrip.Helpers;
using IronGrip.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();
string connectionString = builder.Configuration.GetConnectionString("SqlServerIronGrip");
builder.Services.AddDbContext<IronGripContext>(options =>
{
    options.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information); ;
});
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AuthRepository>();
builder.Services.AddTransient<TagRepository>();
builder.Services.AddTransient<EntrenamientoRepository>();
builder.Services.AddTransient<EjercicioRepository>();
builder.Services.AddTransient<EjercicioHechoRepository>();
builder.Services.AddSingleton<HelperPathProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var supportedCultures = new[]
{
    new CultureInfo("es"),
    new CultureInfo("en") 
};

app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();

using Chunya5.Data;
using Chunya5.Servers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextPool<MyDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysqlDbconn"),
        new MySqlServerVersion(new Version(8, 0, 26)));
});
builder.Services.AddScoped<LiquidationServer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=login}");

app.Run();

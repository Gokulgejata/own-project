 using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Vopflag.Application.Contracts.Persistance;
using Vopflag.Application.Contracts.Persistence;
using Vopflag.Infrastructure.Common;
using Vopflag.Infrastructure.Repositories;
using Vopflag.Infrastructure.UnitOFWork;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#region DATA SEEDING
static async void UpdateDatabaseAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {

        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }
            await SeedData.SeedDataAsync(context);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger< Program >> ();
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");

        }
    }
}
#endregion
var app = builder.Build();
UpdateDatabaseAsync(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhotoLab.Areas.Identity.Data;
using PhotoLab.Data;
using PhotoLab.Interfaces;
using PhotoLab.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddDbContext<PhotoLabContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoLabContextConnection")));

builder.Services.AddDefaultIdentity<PhotoLabUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<PhotoLabContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRequestLocalization("en-US");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PhotoLabContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

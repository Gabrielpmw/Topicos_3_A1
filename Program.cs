using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestauranteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<RestauranteContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<RestauranteContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        Console.WriteLine(">>> SUCESSO: Banco de dados resetado e recriado! <<<");
    }
    catch (Exception ex)
    {
        Console.WriteLine($">>> ERRO ao resetar o banco: {ex.Message} <<<");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
using Microsoft.EntityFrameworkCore;
using restaurante.Data; // Importante para achar o seu Context

var builder = WebApplication.CreateBuilder(args);

// --- ADICIONE ESTA LINHA ABAIXO ---
builder.Services.AddDbContext<RestauranteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// ----------------------------------

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURAÇÃO DO BANCO
builder.Services.AddDbContext<RestauranteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

// 2. CONFIGURAÇÃO DO IDENTITY (Com suporte a Roles)
builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options => {
    options.Password.RequireDigit = false; // Facilitando para teste
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<RestauranteContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// 3. RESET DO BANCO E CRIAÇÃO DE ROLES/ADM
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<RestauranteContext>();
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

        // Reseta o banco
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // CRIAÇÃO DAS ROLES (Papéis)
        string[] nomesRoles = { "Admin", "Comum" };
        foreach (var nomeRole in nomesRoles)
        {
            if (!await roleManager.RoleExistsAsync(nomeRole))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(nomeRole));
            }
        }

        // CRIAÇÃO DO ADM PADRÃO (Para você testar o sistema)
        var adminUser = new Usuario
        {
            UserName = "admin",
            Email = "admin@restaurante.com",
            Nome = "Administrador",
            Sobrenome = "Sistema",
            CPF = "00000000000",
            IsAtivo = true
        };

        var user = await userManager.FindByNameAsync("admin");
        if (user == null)
        {
            var createAdmin = await userManager.CreateAsync(adminUser, "Admin@123");
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        Console.WriteLine(">>> SUCESSO: Banco recriado com Roles e ADM configurados! <<<");
    }
    catch (Exception ex)
    {
        Console.WriteLine($">>> ERRO ao configurar o banco: {ex.Message} <<<");
    }
}

// 4. MIDDLEWARES
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Importante: Authentication sempre antes de Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURAÇÃO DO BANCO
builder.Services.AddDbContext<RestauranteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

// 2. CONFIGURAÇÃO DO IDENTITY (Com suporte a Roles)
builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options => {
    // Como a validação de senha (Maiúscula, número, etc) agora é feita 
    // em tempo real pelo JavaScript, desligamos as travas do C# para evitar erros.
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<RestauranteContext>()
.AddDefaultTokenProviders(); // Importante para o "Esqueci a senha"

var app = builder.Build();

// 3. RESET DO BANCO E CRIAÇÃO DE ROLES/ADM/CLIENTES/CARDÁPIO
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<RestauranteContext>();
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

        // Reseta o banco totalmente
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

        // 1. CRIAÇÃO DO ADM PADRÃO (ID 1)
        var adminUser = new Usuario
        {
            UserName = "admin",
            Email = "admin@restaurante.com",
            PhoneNumber = "00000000000", // Adicionado para evitar erro de campo vazio/único
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

        // 2. CRIAÇÃO DO PEDRO (ID 2)
        var pedroUser = new Usuario
        {
            UserName = "pedro",
            Email = "pedro@gmail.com",
            PhoneNumber = "63988887777",
            Nome = "Pedro",
            Sobrenome = "Vieira Araujo",
            CPF = "11122233344",
            IsAtivo = true
        };
        if (await userManager.FindByNameAsync("pedro") == null)
        {
            var createPedro = await userManager.CreateAsync(pedroUser, "Pedro@123");
            if (createPedro.Succeeded)
            {
                await userManager.AddToRoleAsync(pedroUser, "Comum");
            }
        }

        // 3. CRIAÇÃO DO LUCAS (ID 3)
        var lucasUser = new Usuario
        {
            UserName = "lucas",
            Email = "lucas@gmail.com",
            PhoneNumber = "63955554444",
            Nome = "Lucas",
            Sobrenome = "Vieira Ribeiro",
            CPF = "55566677788",
            IsAtivo = true
        };
        if (await userManager.FindByNameAsync("lucas") == null)
        {
            var createLucas = await userManager.CreateAsync(lucasUser, "Lucas@123");
            if (createLucas.Succeeded)
            {
                await userManager.AddToRoleAsync(lucasUser, "Comum");
            }
        }

        // ====================================================
        // EXECUÇÃO DO ARQUIVO IMPORT.SQL
        // ====================================================
        var scriptPath = Path.Combine(AppContext.BaseDirectory, "import.sql");
        if (File.Exists(scriptPath))
        {
            var sql = File.ReadAllText(scriptPath);
            // Executa o SQL bruto do seu arquivo
            context.Database.ExecuteSqlRaw(sql);
            Console.WriteLine(">>> SUCESSO: import.sql executado e banco populado! <<<");
        }
        else
        {
            Console.WriteLine(">>> AVISO: import.sql não encontrado. Verifique as propriedades do arquivo! <<<");
        }
        // ====================================================

        Console.WriteLine(">>> SUCESSO: Banco recriado com Roles, Usuários Iniciais e Cardápio! <<<");
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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
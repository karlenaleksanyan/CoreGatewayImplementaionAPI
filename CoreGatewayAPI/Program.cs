using CoreGateway.BusinessLogic.ServiceCollections;
using CoreGateway.DBMap;
using CoreGatewayAPI.Handlers;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureServices(builder.Configuration);

//For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
     .AddEntityFrameworkStores<CoreGatewayDbContext>()
     .AddDefaultTokenProviders();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerAuthentication();

var app = builder.Build();

// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await AuthenticationHandler.SeedRolesAndUsersAsync(userManager, roleManager);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
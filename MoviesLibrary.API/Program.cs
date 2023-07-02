using MoviesLibrary.Core.Repositories;
using MoviesLibrary.Core.Models;
using MoviesLibrary.Core.Settings;
using MoviesLibrary.EF.Data;
using MoviesLibrary.EF.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MoviesLibrary.EF.Seeds;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;
using MoviesLibrary.Core;
using MoviesLibrary.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.SaveToken = false;
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddExpressiveAnnotations();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<SeedRole>();
builder.Services.AddTransient<SeedAdmin>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//builder.Services.AddTransient<IGenereRepository, GenereRepository>();
//builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

var Factor = app.Services.GetService<IServiceScopeFactory>();
using var Scope = app.Services.CreateScope();

await Scope.ServiceProvider.GetRequiredService<SeedRole>().SeedAsync();
await Scope.ServiceProvider.GetRequiredService<SeedAdmin>().SeedAsync();

app.MapControllers();

app.Run();
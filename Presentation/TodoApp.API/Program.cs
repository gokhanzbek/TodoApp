using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoApp.Application;
using TodoApp.Persistence;
using TodoApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- SERVÝSLER (BUILDER) ---

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Katmanlý mimari kayýtlarý
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddHttpContextAccessor();

// 1. CORS POLÝTÝKASINI TANIMLA (Frontend eriþimi için þart!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// JWT Ayarlarý
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!))
    };
});

var app = builder.Build();

// --- PIPELINE (APP) ---

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 2. CORS KULLANIMI (Sýralama çok önemli!)
app.UseCors("AllowAll");

// 3. KÝMLÝK VE YETKÝ KONTROLÜ
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
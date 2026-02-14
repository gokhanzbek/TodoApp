using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoApp.Persistence; // 1. BURAYI EKLE (Senin ServiceRegistration burada)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 2. KRÝTÝK EKSÝK BURASIYDI!
// Yazdýðýn veritabaný ve Identity servislerini burada sisteme dahil ediyoruz.
builder.Services.AddPersistenceServices(builder.Configuration);

// JWT Ayarlarý
builder.Services.AddAuthentication("admin") // Default þema
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
            // Güvenlik için null kontrolü (!) ekledim
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 3. BUNU DA EKLE
// Authorization'dan önce Authentication gelmeli ki "Kim bu?" diye bakabilsin.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
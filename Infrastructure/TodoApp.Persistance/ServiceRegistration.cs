using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities.Identity;
using TodoApp.Persistence.Contexts;


namespace TodoApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Veritabanı bağlantısı
            services.AddDbContext<TodoAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2. IDENTITY AYARLARI (Sadece bir tane kalmalı!)
            // Eğer projenizde "AppUser" kullanıyorsanız ApplicationUser olan bloğu tamamen silin.
            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                // Şifre politikalarınız
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<TodoAppDbContext>()
            .AddDefaultTokenProviders();

            // DİKKAT: Alttaki ikinci AddIdentity çağrısını sildik!
        }
    }
}


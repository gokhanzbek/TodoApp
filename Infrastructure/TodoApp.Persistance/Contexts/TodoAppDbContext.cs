using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Entities.Identity;
using TodoApp.Persistence.Identity;

namespace TodoApp.Persistence.Contexts
{
    public class TodoAppDbContext
        : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasOne(x => x.User)
                      .WithMany(u => u.TodoItems)
                      .HasForeignKey(x => x.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Soft delete global filter (ileride repo işini kolaylaştırır)
                entity.HasQueryFilter(x => !x.IsDeleted);
            });
        }
    }
}
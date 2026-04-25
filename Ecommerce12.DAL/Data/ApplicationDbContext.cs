using Ecommerce12.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // public ApplicationDbContext() :base() { }
        public DbSet<Category> categories {  get; set; }
        public DbSet<CategoryTranslation> categoriesTranslation { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet <ProductTranslation> productTranslations { get; set; }
        public DbSet <ProductImage> SubImages { get; set; }
        public DbSet <Cart> carts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IHttpContextAccessor httpContextAccessor)
        : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseModel>();
            var UserID = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(x => x.CreatedBy).CurrentValue = UserID;
                    entityEntry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(x => x.UpdatedBy).CurrentValue = UserID;
                    entityEntry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        // change Table Names 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            modelBuilder.Entity<Category>()
           .HasOne(c => c.User)
           .WithMany()
           .HasForeignKey(c => c.CreatedBy)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cart>()
          .HasOne(c => c.User)
          .WithMany()
          .HasForeignKey(c => c.UserId)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
           .HasOne(c => c.User)
           .WithMany()
           .HasForeignKey(c => c.CreatedBy)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
           .HasOne(c => c.User)
           .WithMany()
           .HasForeignKey(c => c.UserId)
           .OnDelete(DeleteBehavior.NoAction);

        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseModel>();
            var UserID = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach ( var entityEntry in entries)
            {
                if(entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(x => x.CreatedBy).CurrentValue = UserID;
                    entityEntry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }else if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(x=>x.UpdatedBy).CurrentValue = UserID;
                    entityEntry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }



    }
}

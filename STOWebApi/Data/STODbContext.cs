using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Entity;
using System.Reflection.Metadata;

namespace STOWebApi.Data
{
	public class STODbContext : DbContext
	{
		public STODbContext(DbContextOptions<STODbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Car> Cars { get; set; }
		
		public DbSet<Order> Orders { get; set; }
		
		public DbSet<Worker> Workers { get; set; }

		public DbSet<Master> Masters { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Worker>()
				.HasOne(e => e.Master)
				.WithOne(e => e.Worker)
				.HasForeignKey<Master>(e => e.WorkerId)
				.HasPrincipalKey<Worker>(e => e.Id)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Cars)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.HasPrincipalKey(e => e.Id)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Order>()
				.HasOne(e => e.User)
				.WithMany(e => e.Orders)
				.HasForeignKey(e => e.UserId)
				.HasPrincipalKey(e => e.Id)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Order>()
				.HasOne(e => e.Car)
				.WithMany(e => e.Orders)
				.HasForeignKey(e => e.CarVincode)
				.HasPrincipalKey(e => e.Vincode)
				.OnDelete(DeleteBehavior.NoAction);


			modelBuilder.Entity<Order>()
				.HasMany(o => o.Masters)
				.WithMany(m => m.Orders)
				.UsingEntity(j => j.ToTable("OrdersMasters"));

			base.OnModelCreating(modelBuilder);
		}
	}
}

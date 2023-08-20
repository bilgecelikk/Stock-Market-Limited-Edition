using Microsoft.EntityFrameworkCore;
using StockMarketWeb.Models;

namespace StockMarketWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<StockData> AllStocks { get; set; }
        public DbSet<IndividualData> IndividualData { get; set; }

        public DbSet<Ticker> Tickers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockData>()
                .HasMany(sd => sd.Datas)
                .WithOne(id => id.StockData)
                .HasForeignKey(id => id.StockDataId)
                .OnDelete(DeleteBehavior.Cascade); // Set cascade delete behavior
        }
    }
}

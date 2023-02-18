using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ReceiptBookDbContext : DbContext
    {

        public ReceiptBookDbContext(DbContextOptions<ReceiptBookDbContext> options) : base(options) { }

        public ReceiptBookDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ReceiptBookTest;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}

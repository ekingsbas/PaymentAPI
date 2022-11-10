using Microsoft.EntityFrameworkCore;
using PaymentAPI.DAL.Models;

namespace PaymentAPI.DAL
{
    public class PaymentContext: DbContext
    {
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "PaymentDB");
        }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}

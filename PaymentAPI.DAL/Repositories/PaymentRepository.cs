using Microsoft.EntityFrameworkCore;
using PaymentAPI.DAL.Contracts;
using PaymentAPI.DAL.Models;

namespace PaymentAPI.DAL.Repositories
{
    /// <summary>
    /// Repository class of Payment
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        protected readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
            
        }

        public virtual async Task<Payment> CreatePayment(Payment payment)
        {
            var creditCard = _context.CreditCards.FirstOrDefault(w => w.Id == payment.CreditCard.Id);

            if (creditCard == null) throw new Exception("Credit card not found");

            var newPayment = new Payment
            {
                CreditCard = creditCard,
                Timestamp = payment.Timestamp == null ? DateTime.UtcNow : payment.Timestamp,
                Amount = payment.Amount,
                Fee = payment.Fee,

            };

            _context.Payments.Add(newPayment);

            await _context.SaveChangesAsync();

            return newPayment;
        }

        public virtual async Task<decimal> GetBalanceById(int id)
        {
            return await _context.Payments.Where(w => w.CreditCard.Id == id).SumAsync( s => s.Amount + s.Fee);
        }

        public virtual async Task<decimal> GetBalanceByNumber(string cardNumber)
        {
            return await _context.Payments.Where(w => w.CreditCard.CardNumber == cardNumber).SumAsync(s => s.Amount + s.Fee);
        }

        public virtual async Task<Payment> GetPayment(int id)
        {
           
            return await _context.Payments.FirstOrDefaultAsync(x => x.Id == id); ;
        }

        public virtual async Task<List<Payment>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

    }
}

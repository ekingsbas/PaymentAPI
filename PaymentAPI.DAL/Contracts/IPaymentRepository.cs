using PaymentAPI.DAL.Models;

namespace PaymentAPI.DAL.Contracts
{
    /// <summary>
    /// Contract for Payment Repository
    /// </summary>
    public interface IPaymentRepository
    {
        public Task<List<Payment>> GetPayments();
        public Task<Payment> GetPayment(int id);

        public Task<decimal> GetBalanceById(int id);
        public Task<decimal> GetBalanceByNumber(string cardNumber);

        public Task<Payment> CreatePayment(Payment payment);
    }
}

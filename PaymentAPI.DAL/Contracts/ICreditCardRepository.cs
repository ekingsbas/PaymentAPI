using PaymentAPI.DAL.Models;

namespace PaymentAPI.DAL.Contracts
{
    /// <summary>
    /// Contract for Credit Card Repository
    /// </summary>
    public interface ICreditCardRepository
    {
        public Task<List<CreditCard>> GetCreditCards();
        public Task<CreditCard> GetCreditCard(int id);
        public Task<CreditCard> GetCreditCard(string number);
        public Task<CreditCard> CreateCreditCard(CreditCard card);
    }
}

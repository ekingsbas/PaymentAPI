using Microsoft.EntityFrameworkCore;
using PaymentAPI.DAL.Contracts;
using PaymentAPI.DAL.Models;

namespace PaymentAPI.DAL.Repositories
{
    /// <summary>
    /// Repository Class of Credit Card
    /// </summary>
    public class CreditCardRepository: ICreditCardRepository
    {
        protected readonly PaymentContext _context;

        public CreditCardRepository(PaymentContext context)
        {
            _context = context;

            //Fill In-Memory data
            var cards = new List<CreditCard>();
            cards.Add(new CreditCard
            {
                CardNumber = "123456789012345"

            });
            cards.Add(new CreditCard
            {
                CardNumber = "123456789012346"

            });
            cards.Add(new CreditCard
            {
                CardNumber = "123456789012347"

            });

            _context.CreditCards.AddRange(cards);
            _context.SaveChanges();
        }


        public virtual async Task<CreditCard> CreateCreditCard(CreditCard card)
        {
            var newCard = new CreditCard
            {
                CardNumber = card.CardNumber
            };

            await _context.CreditCards.AddAsync(newCard);

            await _context.SaveChangesAsync();

            return newCard;
        }

        public virtual async Task<CreditCard> GetCreditCard(int id)
        {
            
            return await _context.CreditCards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<CreditCard> GetCreditCard(string number)
        {
            return await _context.CreditCards.FirstOrDefaultAsync(x => x.CardNumber == number);
        }

        public virtual async Task<List<CreditCard>> GetCreditCards()
        {
            return await _context.CreditCards.ToListAsync();
        }
    }
}

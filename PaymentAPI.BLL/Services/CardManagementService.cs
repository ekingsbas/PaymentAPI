using PaymentAPI.BLL.Contracts;
using PaymentAPI.DAL.Contracts;
using PaymentAPI.DAL.Models;
using PaymentAPI.DAL.Repositories;
using PaymentAPI.Shared.DTO;

namespace PaymentAPI.BLL.Services
{
    /// <summary>
    /// Service class for Card management
    /// </summary>
    public class CardManagementService
    {
        private readonly ICreditCardRepository _cardRepo;
        private readonly IPaymentRepository _payRepo;
        private readonly IUFEService _feeService;

        public CardManagementService()
        {

        }

        public CardManagementService(CreditCardRepository cardRepo, PaymentRepository payRepo, UFEService feeService)
        {
            _cardRepo = cardRepo;
            _payRepo = payRepo;
            _feeService = feeService;
        }

        /// <summary>
        /// Function for Credit card creation
        /// </summary>
        /// <param name="card">DTO for Credit Card</param>
        /// <returns>DTO of Credit card created</returns>
        /// <exception cref="ArgumentNullException">Card number missing</exception>
        public async Task<CreditCardDTO> Create(CreditCardDTO card)
        {
            try
            {
                if (card == null || string.IsNullOrEmpty( card.CardNumber))
                    throw new ArgumentNullException("Card number missing");

                var previousCard = await _cardRepo.GetCreditCard(card.CardNumber);

                if (previousCard != null)
                    throw new Exception("Credit card already exists");

                CreditCard newCard = new CreditCard {CardNumber = card.CardNumber};

                var result = await _cardRepo.CreateCreditCard(newCard);
                card.Id = result.Id;
                return card;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Function to get a credit card balance
        /// </summary>
        /// <param name="card">DTO for Credit Card</param>
        /// <returns>Balance Amount</returns>
        /// <exception cref="ArgumentNullException">Card Id missing</exception>
        public async Task<decimal?> GetBalance(CreditCardDTO card)
        {
            if (card == null || string.IsNullOrEmpty(card.CardNumber))
                throw new ArgumentNullException("Card Id missing");

            var result = await _payRepo.GetBalanceByNumber(card.CardNumber);

            return result;
        }

        /// <summary>
        /// Function for registering a payment for a credit card
        /// </summary>
        /// <param name="payment">DTO for Payment</param>
        /// <returns>DTO of registered payment</returns>
        /// <exception cref="ArgumentNullException">Card number missing</exception>
        public async Task<PayCardDTO> Pay(PayCardDTO payment)
        {
            if (payment == null || string.IsNullOrEmpty( payment.CardNumber))
                throw new ArgumentNullException("Card number missing");

            var card = await _cardRepo.GetCreditCard(payment.CardNumber);

            decimal fee = await _feeService.GetFee();

            var newPayment = new Payment
            {
                CreditCard = card,
                Amount = payment.Amount,
                Timestamp = payment.Timestamp,
                Fee = fee
            };


            var result = await _payRepo.CreatePayment(newPayment);

            return new PayCardDTO
            {
                PaymentID = result.Id,
                Amount = result.Amount,
                CardNumber = result.CreditCard.CardNumber,
                Timestamp = result.Timestamp,
                Fee = result.Fee,
                Id = result.CreditCard.Id
            };
        }
    }
}

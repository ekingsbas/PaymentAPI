using PaymentAPI.BLL.Contracts;

namespace PaymentAPI.BLL.Services
{
    /// <summary>
    /// Service class for 'Universal Fees Exchange'
    /// </summary>
    public class UFEService : IUFEService
    {
        private decimal lastFee = 0;
        private DateTime lastUpdateTime = DateTime.Now;

        /// <summary>
        /// FUnction for getting the Fee amount
        /// </summary>
        /// <returns>Fee Amount</returns>
        public async Task<decimal> GetFee()
        {
            TimeSpan span = DateTime.Now - lastUpdateTime;
            
            if (span.TotalHours >= 1) 
            {
                lastUpdateTime = DateTime.Now;

                Random random = new Random();
                decimal factor =  (decimal)(random.NextDouble() * 2);

                lastFee = lastFee * factor;

               
            }

            return lastFee;

        }
    }
}

namespace PaymentAPI.DAL.Models
{
    /// <summary>
    /// Model of Payment
    /// </summary>
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }

        public CreditCard CreditCard { get; set; }
    }
}

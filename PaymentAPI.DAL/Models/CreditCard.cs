namespace PaymentAPI.DAL.Models
{
    /// <summary>
    /// Model of Credit Card
    /// </summary>
    public class CreditCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }

        public List<Payment> Payments { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Shared.DTO
{
    /// <summary>
    /// DTO Model class of Peyment
    /// </summary>
    public class PayCardDTO: BaseCardDTO
    {
        public int PaymentID { get; set; }
        [Required(ErrorMessage = "Please enter the card number")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }

        public DateTime Timestamp { get; set; }

    }
}

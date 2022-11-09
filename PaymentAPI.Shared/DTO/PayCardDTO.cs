using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.Shared.DTO
{
    public class PayCardDTO: BaseCardDTO
    {
        [Required(ErrorMessage = "Please enter the card number")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Payment { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

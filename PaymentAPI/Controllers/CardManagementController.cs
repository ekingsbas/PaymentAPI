using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.BLL.Services;
using PaymentAPI.Models;
using PaymentAPI.Shared.DTO;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CardManagementController: ControllerBase
    {
        private readonly ILogger<CardManagementController> _logger;
        private readonly CardManagementService _service;

        public CardManagementController(CardManagementService service, ILogger<CardManagementController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("/balance/{cardNumber}")]
        public async Task<IActionResult> Balance(BaseCardDTO card)
        {
            try
            {
                var balance = await _service.GetBalance(card);

                if (balance == null)
                    throw new Exception("Balance is null");

                var result = new GenericResponse
                {
                    Error = false,
                    Message = balance.ToString()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericResponse
                {
                    Error = true,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("/create/{cardNumber}")]
        public async Task<IActionResult> Create(BaseCardDTO card)
        {
            try
            {
                var newCard = await _service.Create(card);

                if (newCard == false)
                    throw new Exception("Cannot create the card");

                var result = new GenericResponse
                {
                    Error = false,
                    Message = $"{card.CardNumber} Card created"
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericResponse
                {
                    Error = true,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("/pay/{cardNumber}")]
        public async Task<IActionResult> Pay(PayCardDTO pay)
        {
            try
            {
                var payResult = await _service.Pay(pay);

                if (payResult == false)
                    throw new Exception("Cannot register the payment");

                var result = new GenericResponse
                {
                    Error = false,
                    Message = "Card payment recorded"
                };

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new GenericResponse
                {
                    Error = true,
                    Message = ex.Message
                });
            }
        }

    }
}

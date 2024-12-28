using Microsoft.AspNetCore.Mvc;

using PS.Services.Interfaces;
using AutoMapper;
using PS.Domain.DTOs;

namespace PS.Presentation.Controllers
{
   [ApiController]
   [Route("paymentRegistration/")]
   public class PaymentRegistrationServiceController : ControllerBase
   {
      protected readonly IPaymentRegistrationService _service;

      protected readonly IMapper _mapper;

      public PaymentRegistrationServiceController(IPaymentRegistrationService service, IMapper mapper)
      {
         _service = service;
         _mapper = mapper;
      }

      [HttpPost]
      public async Task<IActionResult> RegisterPaymentIntent([FromBody] PaymentIntentDto paymentIntent)
      {
         try
         {
            if (paymentIntent.Amount <= 0) throw new ArgumentException($"'Amount' cant be 0 or negative");

            await _service.RegisterPaymentIntent(paymentIntent);

            return CreatedAtAction(nameof(RegisterPaymentIntent), new { id = paymentIntent.Uuid }, paymentIntent.Uuid);
         }
         catch (ArgumentException ex)
         {
            return BadRequest(new { Message = "Invalid input.", Details = ex.Message });
         }
         catch (Exception ex)
         {
            return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
         }

      }

   }
}
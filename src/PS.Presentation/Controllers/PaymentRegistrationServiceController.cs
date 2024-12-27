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
            await _service.RegisterPaymentIntent(paymentIntent);

            return CreatedAtAction(nameof(RegisterPaymentIntent), new { id = paymentIntent.Uuid }, paymentIntent.Uuid);
         }
         catch (System.Exception e)
         {
            Console.WriteLine(e.Message);
            throw;
         }

      }

   }
}
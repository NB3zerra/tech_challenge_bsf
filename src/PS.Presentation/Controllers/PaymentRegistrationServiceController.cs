using Microsoft.AspNetCore.Mvc;

using PS.Services.Interfaces;
using AutoMapper;
using PS.Domain.DTOs;

namespace PS.Presentation.Controllers
{
   /// <summary>
   /// API para registrar novos PaymentIntents.
   /// </summary>
   [ApiController]
   [Route("payment-intent/")]
   public class PaymentRegistrationServiceController : ControllerBase
   {
      protected readonly IPaymentRegistrationService _service;

      protected readonly IMapper _mapper;

      /// <summary>
      /// Inicializa o controlador.
      /// </summary>
      /// <param name="service">Serviço de registro de pagamentos.</param>
      /// <param name="mapper">Serviço de mapeamento de objetos</param>
      public PaymentRegistrationServiceController(IPaymentRegistrationService service, IMapper mapper)
      {
         _service = service;
         _mapper = mapper;
      }

      /// <summary>
      /// Registra um novo PaymentIntent.
      /// </summary>
      /// <param name="paymentIntent">Dados do PaymentIntent a ser registrado.</param>
      /// <returns>UUID do PaymentIntent criado.</returns>
      /// <response code="201">PaymentIntent criado com sucesso.</response>
      /// <response code="400">Dados inválidos.</response>
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]

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
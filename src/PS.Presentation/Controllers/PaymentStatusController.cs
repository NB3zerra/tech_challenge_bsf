using Microsoft.AspNetCore.Mvc;
using PS.Services.Interfaces;

namespace PS.Presentation.Controllers
{
    /// <summary>
    /// API para consultar o status de PaymentIntents.
    /// </summary>
    [ApiController]
    [Route("api/payment-intent")]
    public class PaymentStatusController : ControllerBase
    {
        protected readonly IPaymentProcessingService _paymentProcessingService;

        /// <summary>
        /// Inicializa o controlador.
        /// </summary>
        /// <param name="paymentProcessingService">Serviço de processamento de pagamentos.</param>
        public PaymentStatusController(IPaymentProcessingService paymentProcessingService)
        {
            _paymentProcessingService = paymentProcessingService;
        }

        /// <summary>
        /// Consulta o status de um PaymentIntent pelo UUID.
        /// </summary>
        /// <param name="paymentIntentId">Identificador único do PaymentIntent.</param>
        /// <returns>Status do PaymentIntent.</returns>
        /// <response code="200">Retorna o status do PaymentIntent.</response>
        /// <response code="404">PaymentIntent não encontrado.</response>
        /// <response code="500">Erro Interno.</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{paymentIntentId}")]
        public async Task<IActionResult> GetPaymentStatus(Guid paymentIntentId)
        {
            try
            {
                var status = await _paymentProcessingService.GetPaymentIntentStatusAsync(paymentIntentId);

                return Ok(new { PaymentIntentId = paymentIntentId, Status = status });

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = "Not Found", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using PS.Services;

namespace PS.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentStatusController : ControllerBase
    {
        protected readonly PaymentProcessingService _paymentProcessingService;

        public PaymentStatusController(PaymentProcessingService paymentProcessingService)
        {   
            _paymentProcessingService = paymentProcessingService;
        }

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
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
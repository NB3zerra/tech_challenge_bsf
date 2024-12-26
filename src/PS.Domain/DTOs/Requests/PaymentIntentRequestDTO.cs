namespace PS.Domain.DTOs.Requests
{
    public class PaymentIntentRequestDTO
    {
        private string? CustomerName { get; set; }
        private double? Amount { get; set; }
        private string? Description { get; set; }
    }
}



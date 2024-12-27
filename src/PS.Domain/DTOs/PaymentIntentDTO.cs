using PS.Domain.Common;

namespace PS.Domain.DTOs
{
    public class PaymentIntentDto 
    {
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public string? CustomerName { get; set; }
        public double? Amount { get; set; }
        public string? Description { get; set; }
        public PaymentIntentStatus Status { get; set; }
    }
}
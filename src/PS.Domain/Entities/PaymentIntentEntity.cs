using System.ComponentModel.DataAnnotations;
using PS.Domain.Common;

namespace PS.Domain.Entities
{
    public class PaymentIntentEntity : BaseEntity
    {
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public double? Amount { get; set; }
        public string? Description { get; set; }
        public PaymentIntentStatus Status { get; set; } = PaymentIntentStatus.UNSET;
    }
}

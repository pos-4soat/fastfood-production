using fastfood_production.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace fastfood_production.Domain.Entity
{
    public class ProductionEntity
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public ProductionStatus Status { get; set; } = ProductionStatus.Awaiting;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
        public IEnumerable<ProductionItemEntity> ProductionItems { get; set; }

        public bool ValidStatus(ProductionStatus status)
        {
            return !NewStatusIsLowerOrEqualCurrentStatus() && !NewStatusIsSkippingSteps();
            bool NewStatusIsLowerOrEqualCurrentStatus()
            => status <= Status;

            bool NewStatusIsSkippingSteps()
            => Status + 1 != status;
        }
    }
}

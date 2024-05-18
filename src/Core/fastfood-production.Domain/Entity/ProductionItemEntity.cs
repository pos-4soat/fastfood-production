using System.ComponentModel.DataAnnotations;

namespace fastfood_production.Domain.Entity;

public class ProductionItemEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int ProductionId { get; set; }
    public ProductionEntity Production { get; set; }
}

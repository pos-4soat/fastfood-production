using fastfood_production.Domain.Entity;
using fastfood_production.Domain.Enum;

namespace fastfood_production.Domain.Contracts.Repository
{
    public interface IProductionRepository
    {
        Task AddProductionAsync(ProductionEntity production, CancellationToken cancellationToken);
        Task EditProductionAsync(ProductionEntity production, CancellationToken cancellationToken);
        Task<IEnumerable<ProductionEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<ProductionEntity?> GetProductionAsync(int orderId, CancellationToken cancellationToken);
        Task<IEnumerable<ProductionEntity>> GetProductionsByStatus(ProductionStatus status, CancellationToken cancellationToken);
    }
}

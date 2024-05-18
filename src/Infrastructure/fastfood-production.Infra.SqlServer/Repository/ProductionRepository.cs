using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using fastfood_production.Domain.Enum;
using fastfood_production.Infra.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace fastfood_production.Infra.SqlServer.Repository
{
    public class ProductionRepository : IProductionRepository
    {
        protected DbSet<ProductionEntity> Data { get; }
        protected ApplicationDbContext _context;

        public ProductionRepository(ApplicationDbContext context)
        {
            _context = context;
            Data = _context.Set<ProductionEntity>();
        }

        public async Task AddProductionAsync(ProductionEntity production, CancellationToken cancellationToken)
        {
            await Data.AddAsync(production, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            production = await Data.Include(x => x.ProductionItems).FirstAsync(x => x.Id == production.Id, cancellationToken: cancellationToken);
        }

        public async Task EditProductionAsync(ProductionEntity production, CancellationToken cancellationToken)
        {
            production.UpdateDate = DateTime.UtcNow;
            Data.Update(production);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductionEntity>> GetAllAsync(CancellationToken cancellationToken)
             => await Data
                .Include(x => x.ProductionItems)
                .OrderBy(x => x.Status)
                .ThenBy(x => x.CreationDate)
                .ToListAsync(cancellationToken);

        public async Task<ProductionEntity?> GetProductionAsync(int orderId, CancellationToken cancellationToken)
            => await Data.Include(x => x.ProductionItems).FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken: cancellationToken);

        public async Task<IEnumerable<ProductionEntity>> GetProductionsByStatus(ProductionStatus status, CancellationToken cancellationToken)
            => await Data
                .Include(x => x.ProductionItems)
                .Where(x => x.Status == status)
                .ToListAsync(cancellationToken: cancellationToken);
    }
}

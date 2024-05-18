using AutoFixture;
using Bogus;
using fastfood_production.Domain.Entity;

namespace fastfood_production.Tests.UnitTests;

public class ModelFakerFactory(Fixture autoFixture, Faker faker)
{
    private readonly Fixture _autoFixture = autoFixture;
    private readonly Faker _faker = faker;

    public TRequest GenerateRequest<TRequest>()
        => _autoFixture.Build<TRequest>()
            .Create();

    public ProductionEntity GenerateProductionEntity()
        => _autoFixture.Build<ProductionEntity>()
            .With(c => c.ProductionItems, GenerateProductionItems())
            .Create();

    public IEnumerable<ProductionItemEntity> GenerateProductionItems()
    {
        List<ProductionItemEntity> list = [];
        for (int i = 0; i < 3; i++)
        {
            list.Add(_autoFixture.Build<ProductionItemEntity>()
            .With(c => c.Production, default(ProductionEntity))
            .Create());
        }
        return list;
    }

    public IEnumerable<TRequest> GenerateManyRequest<TRequest>()
        => _autoFixture.CreateMany<TRequest>();
}

namespace fastfood_production.Infra.Http.Dto
{
    public class OrderResponse
    {
        public OrderData body { get; set; }
    }

    public class OrderData
    {
        public int Status { get; set; }
    }
}

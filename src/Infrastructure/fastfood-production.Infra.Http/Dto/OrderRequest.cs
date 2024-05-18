namespace fastfood_production.Infra.Http.Dto
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public OrderRequest(int OrderId, int StatusOrder)
        {
            Id = OrderId;
            Status = StatusOrder;
        }
    }
}

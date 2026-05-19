namespace ProductionSystem.Domain.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public string CustomerTitle { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string DeliveryDate { get; set; }
    }

    public class CreateOrderDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string DeliveryDate { get; set; }
    }

    public class EditOrderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string DeliveryDate { get; set; }
    }
}

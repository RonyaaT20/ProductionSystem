namespace ProductionSystem.Domain.Entities
{
    public class ProductParameterItem : BaseEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductParameterId { get; set; }
        public ProductParameter ProductParameter { get; set; }

        public int? ParameterValueId { get; set; }
        public ParameterValue ParameterValue { get; set; }
    }
}
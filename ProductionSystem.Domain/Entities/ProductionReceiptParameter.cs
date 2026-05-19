namespace ProductionSystem.Domain.Entities
{
    public class ProductionReceiptParameter : BaseEntity
    {
        public int Id { get; set; }

        public int ProductionReceiptId { get; set; }
        //[ForeignKey(nameof(ProductionReceiptId))]
        public ProductionReceipt ProductionReceipt { get; set; }

        public int ProductParameterId { get; set; }
        //[ForeignKey(nameof(ProductParameterId))]
        public ProductParameter ProductParameter { get; set; }

        public int ParameterValueId { get; set; }
        //[ForeignKey(nameof(ParameterValueId))]
        public ParameterValue ParameterValue { get; set; }
    }
}

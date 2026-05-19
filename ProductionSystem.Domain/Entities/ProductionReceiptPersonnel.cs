namespace ProductionSystem.Domain.Entities
{
    public class ProductionReceiptPersonnel : BaseEntity
    {
        public int Id { get; set; }

        public int ProductionReceiptId { get; set; }
        public ProductionReceipt ProductionReceipt { get; set; }

        public int PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
    }
}

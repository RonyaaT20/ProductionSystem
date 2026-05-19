namespace ProductionSystem.Domain.Entities
{
    public class WasteReceiptPersonnel : BaseEntity
    {
        public int Id { get; set; }

        public int WasteReceiptId { get; set; }
        public WasteReceipt WasteReceipt { get; set; }

        public int PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
    }
}

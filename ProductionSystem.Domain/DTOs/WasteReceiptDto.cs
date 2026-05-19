using System.Collections.Generic;

namespace ProductionSystem.Domain.DTOs
{
    public class WasteReceiptDto
    {
        public int Id { get; set; }
        public string OrderTitle { get; set; }
        public string ProductTitle { get; set; }
        public string ParameterTitle { get; set; }
        public string ParameterValueTitle { get; set; }
        public decimal Quantity { get; set; }
        public string CreatedAt { get; set; }
    }

    public class CreateWasteReceiptDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductParameterId { get; set; }
        public int? ParameterValueId { get; set; }
        public decimal Quantity { get; set; }
        public List<int> PersonnelIds { get; set; }
    }
}
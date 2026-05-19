using System.Collections.Generic;

namespace ProductionSystem.Models
{
    public class ProductionReceiptDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductParameterId { get; set; }
        public int? ParameterValueId { get; set; }
        public decimal Quantity { get; set; }
        public List<int> PersonnelIds { get; set; }
    }
}
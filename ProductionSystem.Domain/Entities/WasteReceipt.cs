using System;
using System.Collections.Generic;

namespace ProductionSystem.Domain.Entities
{
    public class WasteReceipt : BaseEntity
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductParameterId { get; set; }
        public ProductParameter ProductParameter { get; set; }

        public int? ParameterValueId { get; set; }
        public ParameterValue ParameterValue { get; set; }

        public ICollection<WasteReceiptPersonnel> Personnel { get; set; }
    }
}
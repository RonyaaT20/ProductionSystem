using System;
using System.Collections.Generic;

namespace ProductionSystem.Domain.Entities
{
    public class ProductionReceipt : BaseEntity
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

       
        public ICollection<ProductionReceiptPersonnel> Personnel { get; set; }
        public ICollection<ProductionReceiptParameter> ProductReceiptParameters { get; set; }
    }
}
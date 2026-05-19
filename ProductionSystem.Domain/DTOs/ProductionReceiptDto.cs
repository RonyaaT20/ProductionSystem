using System.Collections.Generic;

namespace ProductionSystem.Domain.DTOs
{
    public class ProductionReceiptDto
    {
        public int Id { get; set; }
        public string OrderTitle { get; set; }
        public string ProductTitle { get; set; }
        public List<ProductParameterItemDto> Parameters { get; set; }

        public decimal Quantity { get; set; }
        public string Barcode { get; set; }
        public string CreatedAt { get; set; }
    }

    public class CreateProductionReceiptDto
    {
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public List<int> PersonnelIds { get; set; }
        public List<ProductReceiptParameterDto> ParameterValues { get; set; }

    }

    public class ProductReceiptParameterDto
    {
        public int ParameterId { get; set; }
        public int ValueId { get; set; }
    }

}
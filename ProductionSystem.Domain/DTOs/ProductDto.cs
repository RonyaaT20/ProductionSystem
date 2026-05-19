using System.Collections.Generic;

namespace ProductionSystem.Domain.DTOs
{
    public class ProductParameterItemDto
    {
        public int ParameterId { get; set; }
        public string ParameterTitle { get; set; }
        public int? ValueId { get; set; }
        public string ValueTitle { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int UnitId { get; set; }
        public string UnitTitle { get; set; }
        public List<ProductParameterItemDto> Parameters { get; set; }
    }

    public class ProductParameterInputDto
    {
        public int ParameterId { get; set; }
        public int? ValueId { get; set; }
    }

    public class CreateProductDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int UnitId { get; set; }
        public List<ProductParameterInputDto> Parameters { get; set; }
    }

    public class EditProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int UnitId { get; set; }
        public List<ProductParameterInputDto> Parameters { get; set; }
    }
}
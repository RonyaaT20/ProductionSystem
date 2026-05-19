using System;
using System.Collections.Generic;

namespace ProductionSystem.Domain.Entities
{
    public class ProductParameter : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }

        public ICollection<ParameterValue> Values { get; set; }

    }
}

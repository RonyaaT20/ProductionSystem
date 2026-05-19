using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.DTOs
{
    public class DeleteValueDto
    {
        public int ParameterId { get; set; }
        public string Value { get; set; }
    }
}

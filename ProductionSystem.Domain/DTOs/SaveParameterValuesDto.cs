using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.DTOs
{
    public class SaveParameterValuesDto
    {
        public int Id { get; set; }
        public List<string> Values { get; set; }
    }
}

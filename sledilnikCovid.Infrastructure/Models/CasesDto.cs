using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Infrastructure.Models
{
    public class CasesDto
    {
        public DateTime Date { get; set; }

        public List<RegionData>? Region { get; set; }
    }
}

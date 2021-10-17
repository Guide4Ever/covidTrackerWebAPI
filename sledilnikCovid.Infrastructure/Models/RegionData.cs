using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Infrastructure.Models
{
    public class RegionData
    {
        public string? RegionName { get; set; }

        public int DailyActiveCases { get; set; }

        public int DeceasedToDate { get; set; }

        public int FirstVaccineToDate { get; set; }

        public int SecondVaccineToDate { get; set; }

    }
}

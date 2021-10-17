using sledilnikCovid.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Application.Contracts
{
    public interface IRegionService
    {
        public Task<List<CasesDto>> FetchDataCases(string? region, DateTime? from, DateTime? to);

        public Task<List<LastweekDto>> FetchDataLastWeek();
    }
}

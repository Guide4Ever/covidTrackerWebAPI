﻿using sledilnikCovid.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Infrastructure.Interfaces
{
    public interface IFormatFetcher
    {
        public Task<List<CasesDto>> FetchCases();

    }
}
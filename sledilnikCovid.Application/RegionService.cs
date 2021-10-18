﻿using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Infrastructure.Interfaces;
using sledilnikCovid.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Application
{
    public class RegionService : IRegionService
    {
        private readonly IFormatFetcher _formatFetcher;

        public RegionService(
            IFormatFetcher formatManager
            )
        {
            _formatFetcher = formatManager;

        }

        public async Task<List<CasesDto>> FetchDataCases(string? region, DateTime? from, DateTime? to)
        {
            var data = await _formatFetcher.FetchCases();
            int regionIndex = data[0].Region.Count;

            var dateFrom = from ?? DateTime.MinValue;
            var dateTo = to ?? DateTime.MaxValue;

            data = data.Where(x => x.Date >= dateFrom && x.Date <= dateTo).ToList();

            data = data.Select(x => new CasesDto
            {
                Date = x.Date,
                Region = x.Region
                    .Where(r => r.RegionName == region)
                    .ToList()
             })
            .Where(y => y.Region.Any()).ToList();


            return data;
        }

        public async Task<List<LastweekDto>> FetchDataLastWeek()
        {
            var data = await _formatFetcher.FetchCases();
            var regionIndex = data[0].Region.Count;

            List<LastweekDto> groupedSums = new List<LastweekDto>();

            for (int i = 0; i < regionIndex; i++)
            {
                int sum = 0;
                string name = "";

                for (int j = data.Count - 7; j < data.Count; j++)
                {
                    var row = data[j];
                    var regionThis = row.Region?[i];

                    name = regionThis.RegionName;
                    sum += regionThis.DailyActiveCases;

                }

                LastweekDto temp = new LastweekDto
                {
                    RegionName = name,
                    LastWeekSum = sum
                };

                groupedSums.Add(temp);
                
            }

            groupedSums = groupedSums.OrderByDescending(x => x.LastWeekSum).ToList();

            return groupedSums;

        }

    }
}

using sledilnikCovid.Application.Contracts;
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

            List<CasesDto> filteredList = new List<CasesDto>();

            //TODO: filtration
            for (int i = 0; i < data.Count; i++)
            {
                var row = data[i];
                var dateThis = row.Date;
                if (dateThis >= dateFrom &&
                    dateThis <= dateTo)
                {
                    if (region != null)
                    {
                        for (int j = 0; j < regionIndex; j++)
                        {
                            if (String.Equals(row.Region?[j].RegionName, region))
                            {
                                var regionData = new List<RegionData>();
                                regionData.Add(row.Region[j]);

                                //Reconstruction
                                filteredList.Add(new CasesDto
                                {
                                    Date = dateThis,
                                    Region = regionData
                                });
                            }
                        }
                    }
                    else
                    {
                        filteredList.Add(row);
                    }
                }

            }

            return filteredList;
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

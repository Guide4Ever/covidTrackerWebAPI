using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Infrastructure.Models;

namespace sledilnikCovid.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class regionController : ControllerBase
    {

        private readonly IRegionService _regionService;

        public regionController(
            IRegionService regionService
            )
        {
            _regionService = regionService;
        }

        /// <summary>
        /// Returns all cases with information about date and region based data
        /// </summary>
        /// <param name="region">Filter cases based on this region.</param>
        /// <param name="from">Filter cases from this date.</param>
        /// <param name="to">Filter cases to this date.</param>
        /// <returns>A list of cases</returns>
        [HttpGet]
        [Route("cases")]
        public async Task<ActionResult<List<CasesDto>>> GetCases(string? region, DateTime? from, DateTime? to)
        {

            var data = await _regionService.FetchDataCases(region, from, to);
            return Ok(data);
        }

        /// <summary>
        /// Returns a list of regions with their sum of active cases for the past 7 days.
        /// </summary>
        /// <returns>A list of regions and appropriate sums</returns>
        [HttpGet]
        [Route("lastweek")]
        public async Task<ActionResult<List<LastweekDto>>> GetLastweek()
        {
            var data = await _regionService.FetchDataLastWeek();
            return Ok(data);
        }

    }
}

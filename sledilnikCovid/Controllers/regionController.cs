using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Infrastructure.Models;

namespace sledilnikCovid.Api.Controllers
{   
    [Authorize]
    [Produces("application/json")]
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
        /// <param name="region"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>A list of cases</returns>
        /// <response code="200">Returns a list of cases</response>
        [HttpGet]
        [Route("cases")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CasesDto>>> GetCases(string? region, DateTime? from, DateTime? to)
        {
            var data = await _regionService.FetchDataCases(region, from, to);
            return Ok(data);
        }

        /// <summary>
        /// Returns a list of regions with their sum of active cases for the past week
        /// </summary>
        /// <returns>A list of regions and their appropriate sums</returns>
        /// <response code="200">Returns a list of regions and their appropriate sums</response>
        [HttpGet]
        [Route("lastweek")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<LastweekDto>>> GetLastweek()
        {
            var data = await _regionService.FetchDataLastWeek();
            return Ok(data);
        }

    }
}

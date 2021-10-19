using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Core.Models;

namespace sledilnikCovid.Api.Controllers
{   
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {

        private readonly IRegionService _regionService;

        public RegionController(
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
        /// <response code="400">Returns an exception based on the issue at hand</response>
        [HttpGet]
        [Route("cases")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CasesDto>>> GetCases(string? region, DateTime? from, DateTime? to)
        {
            //collect syntax and semantic errors
            var validRegions = new List<string> {"lj", "ce", "kr", "nm", "kk", "kp", "mb", "ms", "ng", "po", "sg", "za"};
            var exceptionStack = new List<string>();
            DateTime dDate;

            if (region != null && !validRegions.Contains(region))
                exceptionStack.Add("Invalid region.");

            if (from > to)
                exceptionStack.Add("'From' date cannot be later than 'To' date");

            //evaluate syntax and semantic errors
            if (exceptionStack.Count == 0)
            {
                var data = await _regionService.FetchDataCases(region, from, to);
                return Ok(data);
            }
            else {
                return BadRequest(exceptionStack);
            }

        }

        /// <summary>
        /// Returns a list of regions with their sum of active cases for the past week
        /// </summary>
        /// <returns>A list of regions and their appropriate sums</returns>
        /// <response code="200">Returns a list of regions and their appropriate sums</response>
        /// <response code="400">Returns an exception based on the issue at hand</response>
        [HttpGet]
        [Route("lastweek")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<LastweekDto>>> GetLastweek()
        {
            var data = await _regionService.FetchDataLastWeek();
            return Ok(data);
        }

    }
}

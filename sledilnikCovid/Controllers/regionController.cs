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


        [HttpGet]
        [Route("cases")]
        public async Task<ActionResult<List<CasesDto>>> GetCases(string? region, DateTime? from, DateTime? to){

            var data = await _regionService.FetchDataCases(region, from, to);
            return Ok(data);
        }

        [HttpGet]
        [Route("lastweek")]
        public async Task<ActionResult<List<LastweekDto>>> GetLastweek(string? region, DateTime? from, DateTime? to)
        {
            var data = await _regionService.FetchDataLastWeek();
            return Ok(data);
        }

    }
}

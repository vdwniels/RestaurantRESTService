using AdresBeheerBL.Model;
using AdresBeheerBL.services;
using AdresBeheerREST.mappers;
using AdresBeheerREST.Model.OUT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdresBeheerREST.Controllers
{
    [Route("api/[controller]/gemeente")]
    [ApiController]
    public class AdresBeheerController : ControllerBase
    {
        private GemeenteService gemeenteService;
        [HttpGet("{gemeenteId}")]
        public ActionResult<GemeenteRESTOutputDTO> GetGemeente(int gemeenteId)
        {
            try
            {
                Gemeente gemeente = gemeenteService.GeefGemeente(gemeenteId);
                return Ok(MapFromDomain.MapFromGemeenteDomain(gemeente));
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

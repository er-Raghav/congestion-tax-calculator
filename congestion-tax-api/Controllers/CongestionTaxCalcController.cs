using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace congestion_tax_api.Controllers
{
    [ApiController]
    [Route("api/congestion-tax")]
    public class CongestionTaxCalcController : ControllerBase

    {

        private readonly ICongestionTaxCalculator _taxCalc;
        private readonly ILogger<CongestionTaxCalcController> _logger;
        public CongestionTaxCalcController(ICongestionTaxCalculator taxCalc, ILogger<CongestionTaxCalcController> logger)
        {
            _taxCalc = taxCalc;
            _logger = logger;
        }


        [HttpPost]
        public IActionResult Post([FromBody] VehicleInfo vinfo)
        {
            try
            {
                _logger.LogInformation("request received!");
                return Ok(_taxCalc.GetTax(vinfo.Vechicle, vinfo.dates));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);                
            }
            
        }
    }
}

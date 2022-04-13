using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using congestion.calculator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace congestion_tax_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxCalcController : ControllerBase

    {

        private readonly ICongestionTaxCalculator _taxCalc;

        public CongestionTaxCalcController(ICongestionTaxCalculator taxCalc)
        {
            _taxCalc = taxCalc;
        }


        [HttpPost]
        public IActionResult Post([FromBody] VehicleInfo vinfo)
        {
            return Ok(_taxCalc.GetTax(vinfo.Vechicle, vinfo.dates));
        }
    }
}

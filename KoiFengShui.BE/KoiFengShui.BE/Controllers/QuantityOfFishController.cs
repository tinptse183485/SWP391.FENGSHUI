using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuantityOfFishController : ControllerBase
    {
        private readonly IKoiVarietyService _koiVarietyService;
        private readonly IQuantityOfFishService _QuantityService;
        private readonly IElementService _elementService;
      

        public QuantityOfFishController(IKoiVarietyService koiVarietyService, IQuantityOfFishService quantityService, IElementService elementService)
        {
            _koiVarietyService = koiVarietyService;
            _QuantityService = quantityService;
            _elementService = elementService;
           
        }
        [HttpGet("GetQuantityOfFishByDOB")]
        public IActionResult GetQuantityByElement(string dob)
        {
            int year = int.Parse(dob.Substring(0, 4));
            try
            {
                string element = _elementService.GetElementByBirthYear(year);
                var quantity = _QuantityService.getQuantityByElement(element);
                return Ok(quantity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompatibilityController : ControllerBase
    {
        private readonly IElementService _elementService;
        private readonly LunarCalendarConverter _lunarCalendarConverter;
        private readonly ILifePlaceService _lifePlaceService;
        private readonly IKoiVarietyService _koiVarietyService;
        private readonly IElementColorService _elementColorService;
        private readonly ITypeColorService _typeColorService;

        public CompatibilityController(IElementService elementService, LunarCalendarConverter lunarCalendarConverter, ILifePlaceService lifePlaceService, IKoiVarietyService koiVarietyService, IElementColorService elementColorService, ITypeColorService typeColorService)
        {
            _elementService = elementService;
            _lunarCalendarConverter = lunarCalendarConverter;
            _lifePlaceService = lifePlaceService;
            _koiVarietyService = koiVarietyService;
            _elementColorService = elementColorService;
            _typeColorService = typeColorService;
        }
        [HttpGet("GetAttribute")]
        public IActionResult GetAttribute(string koiType, string dob)
        {
            int year = int.Parse(dob.Substring(0, 4));

            try
            {
                string element = _elementService.GetElementByBirthYear(year);

        
                var koiColors = _typeColorService.GetColorsAndPercentages(koiType);

                double totalScore = 0;

                foreach (var color in koiColors)
                {
                    var pointForColor = _elementColorService.GetPointElementColor(element, color.ColorId);
                    totalScore += pointForColor * color.Percentage;
                }
             
                return Ok(Math.Round(totalScore, 3));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        public class CustomKoiTypeColor
        {
            public string KoiType { get; set; }
            public List<CustomColor> Colors { get; set; }
        }

        public class CustomColor
        {
            public string ColorId { get; set; }
            public double Percentage { get; set; }
        }

        [HttpPost("GetAttributeCustom")]
        public IActionResult GetAttributeCustom([FromBody] CustomKoiTypeColor customKoiType, string dob)
        {
            int year = int.Parse(dob.Substring(0, 4));

            try
            {
                string element = _elementService.GetElementByBirthYear(year);

                
                double totalScore = 0;
                double totalPercentage = 0;

                foreach (var color in customKoiType.Colors)
                {
                    

                    var pointForColor = _elementColorService.GetPointElementColor(element, color.ColorId);
                    totalScore += pointForColor * (color.Percentage);
                    totalPercentage += color.Percentage;
                }

                // Kiểm tra tổng phần trăm
                if (Math.Abs(totalPercentage ) > 1.00) // Cho phép sai số nhỏ
                {
                    return BadRequest("Total percentage must be 100%");
                }

                return Ok(Math.Round(totalScore, 3));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

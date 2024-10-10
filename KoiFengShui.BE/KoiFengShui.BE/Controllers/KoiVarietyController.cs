using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

using System.Xml.Linq;

namespace KoiFengShui.BE.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class KoiVarietyController : ControllerBase
    {
        private readonly IKoiVarietyService _koiVarietyService;
        private readonly IQuantityOfFishService _QuantityService;
        private readonly IElementService _elementService;
        private readonly IColorService _colorService;
        private readonly ITypeColorService _typeColorService;
        private readonly IElementColorService _elementColorService;


        public KoiVarietyController(IKoiVarietyService koiVarietyService, IQuantityOfFishService quantityService, IElementService elementService, IColorService colorService, ITypeColorService typeColorService, IElementColorService elementColorService)
        {
            _koiVarietyService = koiVarietyService;
            _QuantityService = quantityService;
            _elementService = elementService;
            _colorService = colorService;
            _typeColorService = typeColorService;
            _elementColorService = elementColorService;
        }
        [HttpGet("GetQuantityByDOB")]
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
        [HttpGet("GetListKoiByDOBOrder")]
        public IActionResult GetListKoiByDOBOrder(string dob)
        {
            List<KoiVariety> listKoi = new List<KoiVariety>();
            int year = int.Parse(dob.Substring(0, 4));
            try
            {
                string element = _elementService.GetElementByBirthYear(year);
                var mutual = _elementService.GetElementAndMutualism(element);
                var list1 = _koiVarietyService.GetKoiVarietiesByElemnet(mutual.Mutualism);
                var list2 = _koiVarietyService.GetKoiVarietiesByElemnet(element);
                 listKoi.AddRange(list1);
                listKoi.AddRange(list2);
                return Ok(listKoi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetListKoiByDOBOrderS1")]
        public IActionResult GetListKoiByDOBOrderS1(string dob)
        {
            int year = int.Parse(dob.Substring(0, 4));
            try
            {
                string element = _elementService.GetElementByBirthYear(year);
                var mutual = _elementService.GetElementAndMutualism(element);
                var list1 = _koiVarietyService.GetKoiVarietiesByElemnet(mutual.Mutualism);
                var list2 = _koiVarietyService.GetKoiVarietiesByElemnet(element);

                var allKoi = new List<KoiVariety>();
                allKoi.AddRange(list1);
                allKoi.AddRange(list2);

                var scoredKoi = new List<(KoiVariety Koi, double Score)>();

                foreach (var koi in allKoi)
                {
                    var koiColors = _typeColorService.GetColorsAndPercentages(koi.KoiType);
                    double totalScore = 0;

                    foreach (var color in koiColors)
                    {
                        var pointForColor = _elementColorService.GetPointElementColor(element, color.ColorId);
                        totalScore += pointForColor * color.Percentage;
                    }

                    scoredKoi.Add((koi, totalScore));
                }

                var sortedKoi = scoredKoi.OrderByDescending(k => k.Score).Select(k => k.Koi).ToList();

                return Ok(sortedKoi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetAllKoi")]
        public IActionResult GetAllKoiVarieties()
        {
            List<KoiVariety> listKoi = new List<KoiVariety>();

            try
            {

                listKoi = _koiVarietyService.GetKoiVarieties();

                return Ok(listKoi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetListKoiByColor")]
        public IActionResult GetListKoiByColor(string color)
        {
            List<KoiVariety> listKoi = new List<KoiVariety>();
            try
            {
                List<TypeColor> listTypeColor = _typeColorService.GetTypeByColor(color);

                foreach (TypeColor typeColor in listTypeColor)
                {

                    KoiVariety koiVariety = _koiVarietyService.GetKoiVarietyByType(typeColor.KoiType);


                    if (koiVariety != null)
                    {
                        listKoi.Add(koiVariety);
                    }
                }

                return Ok(listKoi); // Return the correct variable
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }

}

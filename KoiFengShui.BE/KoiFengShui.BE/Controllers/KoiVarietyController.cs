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

        public KoiVarietyController(IKoiVarietyService koiVarietyService, IQuantityOfFishService quantityService, IElementService elementService, IColorService colorService, ITypeColorService typeColorService)
        {
            _koiVarietyService = koiVarietyService;
            _QuantityService = quantityService;
            _elementService = elementService;
            _colorService = colorService;
            _typeColorService = typeColorService;
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
        [HttpGet("GetListKoiByDOB")]
        public IActionResult GetKoiVarietiesByElement(string dob)
        {
            List<KoiVariety> listKoi = new List<KoiVariety>();
            int year = int.Parse(dob.Substring(0, 4));
            try
            {
                string element = _elementService.GetElementByBirthYear(year);
                var mutual = _elementService.GetElementAndMutualism(element);
                var list1 = _koiVarietyService.GetKoiVarietiesByElemnet(element);
                listKoi.AddRange(list1);
                var list2 = _koiVarietyService.GetKoiVarietiesByElemnet(mutual.Mutualism);
                listKoi.AddRange(list2);
                return Ok(listKoi);
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

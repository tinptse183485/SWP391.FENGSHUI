using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace KoiFengShui.BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class KoiVarietyController : ControllerBase
	{
		private readonly IKoiVarietyService _koiVarietyService;
		private readonly IQuantityService _QuantityService;
		private readonly IElementService _elementService;

		public KoiVarietyController(IKoiVarietyService koiVarietyService,IQuantityService quantityService, IElementService elementService)
		{
			_koiVarietyService = koiVarietyService;
			_QuantityService = quantityService;
			_elementService = elementService;
		}

		[HttpGet("GetListKoi")]
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

		[HttpGet("GetQuantity")]
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

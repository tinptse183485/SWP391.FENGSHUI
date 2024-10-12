using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FateController : ControllerBase
	{
		private readonly IElementService _elementService;
		private readonly LunarCalendarConverter _lunarCalendarConverter;
		private readonly ILifePlaceService _lifePlaceService;

		public FateController(IElementService elementService, LunarCalendarConverter lunarCalendarConverter, ILifePlaceService lifePlaceService)
		{
			_elementService = elementService;
			_lunarCalendarConverter = lunarCalendarConverter;
			_lifePlaceService = lifePlaceService;
		}

		[HttpGet("element")]
		public IActionResult GetElementByBirthYear(string dob)
		{
			int year = int.Parse(dob.Substring(0, 4));
			try
			{
				var element = _elementService.GetElementByBirthYear(year);
				return Ok(element);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
			}
		}
		[HttpGet("Mutualism")]
		public IActionResult GetMutualismByElement(string e)
		{
			try
			{
				var element = _elementService.GetElementAndMutualism(e);
				return Ok(element);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
			}
		}
		[HttpGet("ConvertToLunar")]
		public IActionResult ConvertToLunar(string YOB)
		{
			try
			{
				int[] lunarDate = LunarCalendarConverter.ConvertSolarToLunar(YOB, 7);
				if (lunarDate == null)
				{
					return BadRequest("Không thể tính toán ngày âm lịch cho ngày đã nhập.");
				}
				return Ok(new { LunarYear = lunarDate[2], LunarMonth = lunarDate[1], LunarDay = lunarDate[0] });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
			}
		}

		[HttpGet("CalculateLife_Palace")]
		public IActionResult CalculateLife_Palace(string YOB, string Gender)
		{
			try
			{
				int[] lunarDate = LunarCalendarConverter.ConvertSolarToLunar(YOB, 7);
				if (lunarDate == null)
				{
					return BadRequest("Không thể tính toán ngày âm lịch cho ngày đã nhập.");
				}
				else
				{
					int lunarYear = lunarDate[2];
					return Ok(_lifePlaceService.CalculateFate(lunarYear, Gender));
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
			}
		}


	}
}

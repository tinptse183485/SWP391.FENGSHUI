using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Reflection;


namespace KoiFengShui.BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PondController : ControllerBase
	{
		private readonly IPointOfShapeService _pointOfShapeService;
		private readonly IShapeService _shapeService;
		private readonly IElementService _elementService;
		private readonly LunarCalendarConverter _lunarCalendarConverter;
		private readonly ILifePlaceDirectionService _lifePlaceDirectionService;
		private readonly ILifePlaceService _lifePlaceService;
		public PondController(IPointOfShapeService pointOfShapeService, IShapeService shapeService, IElementService elementService, LunarCalendarConverter lunarCalendarConverter, ILifePlaceDirectionService lifePlaceDirectionService, ILifePlaceService lifePlaceService)

		{
			_pointOfShapeService = pointOfShapeService;
			_shapeService = shapeService;
			_elementService = elementService;
			_lunarCalendarConverter = lunarCalendarConverter;

			_lifePlaceDirectionService = lifePlaceDirectionService;
			_lifePlaceService = lifePlaceService;


		}

		[HttpGet("GetGoodShapeByDOB")]
		public IActionResult GetShapeOfPondByElement(string dob)
		{
			List<Shape> listPond = new List<Shape>();
			int year = int.Parse(dob.Substring(0, 4));
			try
			{
				string element = _elementService.GetElementByBirthYear(year);
				var listShape = _pointOfShapeService.GetGoodShapeByElemnet(element);
				foreach (PointOfShape shape in listShape)
				{
					Shape pond = _shapeService.GetShapeById(shape.ShapeId);
					if (pond != null)
					{
						listPond.Add(pond);
					}
				}
				return Ok(listPond);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetGoodDirectionByDOB")]
		public IActionResult GetDirectionOfPondByElement(string dob, string Gender)
		{
			List<Shape> listPond = new List<Shape>();
			string Life_Palace = "";
			try
			{
				int[] lunarDate = LunarCalendarConverter.ConvertSolarToLunar(dob, 7);
				if (lunarDate == null)
				{
					return BadRequest("Không thể tính toán ngày âm lịch cho ngày đã nhập.");
				}
				else
				{
					int lunarYear = lunarDate[2];
					Life_Palace = _lifePlaceService.CalculateFate(lunarYear, Gender);
				}
				var listdirection = _lifePlaceDirectionService.GetGoodDirectionByLifePalace(Life_Palace);
				return Ok(listdirection);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}

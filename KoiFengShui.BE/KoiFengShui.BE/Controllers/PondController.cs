using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public PondController(IPointOfShapeService pointOfShapeService, IShapeService shapeService, IElementService elementService, LunarCalendarConverter lunarCalendarConverter)
		{
			_pointOfShapeService = pointOfShapeService;
			_shapeService = shapeService;
			_elementService = elementService;
			_lunarCalendarConverter = lunarCalendarConverter;
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


    }

}

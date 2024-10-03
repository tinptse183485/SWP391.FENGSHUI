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
        private readonly IPointOfShapeService _pointOfShapeService;
        private readonly ILifePlaceDirectionService _lifePlaceDirectionService;

        public CompatibilityController(IElementService elementService, LunarCalendarConverter lunarCalendarConverter, ILifePlaceService lifePlaceService, IKoiVarietyService koiVarietyService, IElementColorService elementColorService, ITypeColorService typeColorService, IPointOfShapeService pointOfShapeService, ILifePlaceDirectionService lifePlaceDirectionService)
        {
            _elementService = elementService;
            _lunarCalendarConverter = lunarCalendarConverter;
            _lifePlaceService = lifePlaceService;
            _koiVarietyService = koiVarietyService;
            _elementColorService = elementColorService;
            _typeColorService = typeColorService;
            _pointOfShapeService = pointOfShapeService;
            _lifePlaceDirectionService = lifePlaceDirectionService;
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

        [HttpGet("GetAttributeCustomColor")]
        public IActionResult GetAttributeCustomColor([FromBody] CustomKoiTypeColor customKoiType, string dob)
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
                if (Math.Abs(totalPercentage) > 1.00) // Cho phép sai số nhỏ
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


		[HttpPost("GetAveragePointOfKoiTypes")]
		public IActionResult GetAveragePointOfKoiTypes([FromBody] List<CustomKoiTypeColor> customKoiTypes, string dob)
		{
			int year = int.Parse(dob.Substring(0, 4));
			try
			{
				string element = _elementService.GetElementByBirthYear(year);
				double totalScore = 0;
				int totalQuantity = 0;

				foreach (var koiType in customKoiTypes)
				{
					double koiScore = 0;
					double totalPercentage = 0;

					foreach (var color in koiType.Colors)
					{
						var pointForColor = _elementColorService.GetPointElementColor(element, color.ColorId);
						koiScore += pointForColor * color.Percentage;
						totalPercentage += color.Percentage;
					}

					if (Math.Abs(totalPercentage - 1.00) > 0.01) // Cho phép sai số nhỏ
					{
						return BadRequest($"Total percentage for koi type {koiType.KoiType} must be 100%");
					}

					totalScore += koiScore;
					totalQuantity++;
				}

				if (totalQuantity == 0)
				{
					return BadRequest("No koi types provided");
				}

				double averageScore = totalScore / totalQuantity;
				return Ok(new { AverageScore = Math.Round(averageScore, 3) });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetPointOfShapeByShapeIDAndDOB")]
        public IActionResult GetPointOfShapeByShapeIDAndDOB(string ShapeID, string DOB)
        {

            int year = int.Parse(DOB.Substring(0, 4));
            try
            {
                string element = _elementService.GetElementByBirthYear(year);
                PointOfShape shape = _pointOfShapeService.GetPointOfShape(element, ShapeID);
                if (shape != null)
                {
                    return Ok(shape.Point);
                }
                else
                {
                    return BadRequest("Không tìm thấy !");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetPointOfDirectionByDirecDOBGEN")]
        public IActionResult GetPointOfDirectionByDirecDOBGEN(string Direction, string DOB, string Gender)
        {
            string Life_Palace = "";
            try
            {
                int[] lunarDate = LunarCalendarConverter.ConvertSolarToLunar(DOB, 7);
                if (lunarDate == null)
                {
                    return BadRequest("Không thể tính toán ngày âm lịch cho ngày đã nhập.");
                }
                else
                {
                    int lunarYear = lunarDate[2];
                    Life_Palace = _lifePlaceService.CalculateFate(lunarYear, Gender);
                }
                var point = _lifePlaceDirectionService.GetLifePlaceDirectionById(Life_Palace, Direction);
                return Ok(point.PointOfDirection);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("GetTheCompatibilityOfUserByListFish")]
        public IActionResult GetTheCompatibilityOfUserByListFish([FromBody] List<CustomKoiTypeColor> customKoiTypes, string ShapeID, string Direction, string DOB, string Gender)
        {
            try
            {
                var s1Result = GetPointOfDirectionByDirecDOBGEN(Direction, DOB, Gender) as OkObjectResult;
                var s2Result = GetPointOfShapeByShapeIDAndDOB(ShapeID, DOB) as OkObjectResult;
                var s3Result = GetAveragePointOfKoiTypes(customKoiTypes, DOB) as OkObjectResult;

                if (s1Result == null || s2Result == null || s3Result == null)
                {
                    return BadRequest("Không thể tính toán một hoặc nhiều thành phần của độ tương thích.");
                }

                double s1 = Convert.ToDouble(s1Result.Value);
                double s2 = Convert.ToDouble(s2Result.Value);
                double s3 = ((dynamic)s3Result.Value).AverageScore;

                double compa = (50 * s1 + 20 * s2 + 30 * s3) ;
                return Ok(new { Compatibility = Math.Round(compa, 2) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("GetTheCompatibilityOfUserByOneFish")]
        public IActionResult GetTheCompatibilityOfUserByOneFish(string koiType, string ShapeID, string Direction, string DOB, string Gender)
        {
            try
            {
                var s1Result = GetPointOfDirectionByDirecDOBGEN(Direction, DOB, Gender) as OkObjectResult;
                var s2Result = GetPointOfShapeByShapeIDAndDOB(ShapeID, DOB) as OkObjectResult;
                var s3Result = GetAttribute(koiType, DOB) as OkObjectResult;

                if (s1Result == null || s2Result == null || s3Result == null)
                {
                    return BadRequest("Không thể tính toán một hoặc nhiều thành phần của độ tương thích.");
                }

                double s1 = Convert.ToDouble(s1Result.Value);
                double s2 = Convert.ToDouble(s2Result.Value);
                double s3 = Convert.ToDouble(s3Result.Value);

                double compa = (50 * s1 + 20 * s2 + 30 * s3);
                return Ok(new { Compatibility = Math.Round(compa, 2) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

using FengShuiKoi_BO;
using FengShuiKoi_Services;
using KoiFengShui.BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        private readonly IElementColorService _elementColorService;
        private readonly ITypeColorService _typeColorService;

        public ColorController(IColorService colorService, IElementColorService elementColorService, ITypeColorService typeColorService)
        {
            _colorService = colorService;
            _elementColorService = elementColorService;
            _typeColorService = typeColorService;
        }

        [HttpGet("GetAllColor")]
        public IActionResult GetAllColor()
        {
            List<Color> listColor = new List<Color>();
           
            try
            {
                listColor = _colorService.GetColors();


                return Ok(listColor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddColor")]
        public IActionResult AddColor(ColorDTO color)
        {
            try
            {
                if (color.ColorId == null || color.ColorId == "")
                {
                    return BadRequest("Please input the color !");

                }

                if (_colorService.GetColorById(color.ColorId) != null)
                {
                    return BadRequest("Has this color already.");

                }
               

       
                var _color = new Color
                {
                   ColorId = color.ColorId,
                };

                bool result = _colorService.AddColor(_color);   
                if (result)
                {
                    return Ok(new { Message = "Tạo màu thành công", _colorID =color.ColorId});
                }
                else
                {
                    return BadRequest("Tạo màu thất bại");
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpDelete("DeleteColor/{colorId}")]
        public IActionResult DeleteColor(string colorId)
        {
            try
            {

                var existingcolorId = _colorService.GetColorById(colorId);
                if (existingcolorId == null)
                {
                    return NotFound("Không tìm thấy màu tương ứng.");
                }
                var existingElementColor = _elementColorService.GetElementColorByColorId(colorId);
                if (existingElementColor != null)
                {
                    return NotFound("Màu này đang được sửa dụng để tính điểm cho mệnh.");
                }
                var existingTypeColors = _typeColorService.GetTypeByColor(colorId);
                if (existingTypeColors != null && existingTypeColors.Any())
                {
                    return BadRequest("Màu này đang được sử dụng để tính điểm cho cá.");
                }
                bool result = _colorService.DeleteColor(colorId);
                if (result)
                {
                    return Ok("Xóa Màu thành công");
                }
                else
                {
                    return BadRequest("Xóa Màu thất bại");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

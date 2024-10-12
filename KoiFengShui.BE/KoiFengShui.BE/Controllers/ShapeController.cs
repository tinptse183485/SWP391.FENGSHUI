using FengShuiKoi_BO;
using FengShuiKoi_Services;
using KoiFengShui.BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapeController : ControllerBase
    {

        private readonly IPointOfShapeService _pointOfShapeService;
        private readonly IShapeService _shapeService;
        private readonly IElementService _elementService;

        public ShapeController(IPointOfShapeService pointOfShapeService, IShapeService shapeService, IElementService elementService)
        {
            _pointOfShapeService = pointOfShapeService;
            _shapeService = shapeService;
            _elementService = elementService;

        }
        [HttpGet("GetAllShape")]
        public IActionResult GetAllShape()
        {
            List<Shape> listShape = new List<Shape>();

            try
            {

                listShape = _shapeService.GetShapes();

                return Ok(listShape);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpGet("GetShapeById")]
        public IActionResult GetShapeById(string shapeID)
        {


            try
            {
                var shape = _shapeService.GetShapeById(shapeID);
                if (shapeID == null)
                {
                    return NotFound("Không tìm thấy quảng cáo");
                }

                return Ok(shape);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpPut("UpdateShape")]
        public IActionResult UpdateShape(ShapeDTO shape)
        {
            try
            {
                var existingShape = _shapeService.GetShapeById(shape.ShapeId);
                if (existingShape == null)
                {
                    return NotFound("Không tìm thấy hồ cá");
                }
                if (shape.Image == "" || shape.Image == null)
                {
                    return BadRequest("Vui lòng nhập hình ảnh.");

                }


                existingShape.Image = shape.Image;


                bool result = _shapeService.UpdateShape(existingShape);
                if (result)
                {
                    return Ok("Cập nhật hồ thành công");
                }
                else
                {
                    return BadRequest("Cập nhật hồ thất bại");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpPost("AddShape")]
        public IActionResult AddShape(ShapeDTO shape)
        {
            try
            {
                if (shape.ShapeId == null || shape.ShapeId == "")
                {
                    return BadRequest("Vui lòng nhập hình dạng.");

                }

                if (shape.Image == "" || shape.Image == null)
                {
                    return BadRequest("Vui lòng nhập hình ảnh.");

                }
                if (_shapeService.GetShapeById(shape.ShapeId) != null)
                {
                    return BadRequest("Hình dạng này đã tồn tại.");

                }
                if (_shapeService.GetShapeByImg(shape.Image) != null)
                {
                    return BadRequest("Hình ảnh này đã tồn tại.");

                }
                var _shape = new Shape
                {
                    ShapeId = shape.ShapeId,
                    Image = shape.Image,
                };

                bool result = _shapeService.AddShape(_shape);
                if (result)
                {
                    return Ok(new { Message = "Tạo hồ cá thành công", ShapeId = shape.ShapeId, Image = shape.Image });
                }
                else
                {
                    return BadRequest("Tạo màu thất bại");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpDelete("DeleteShape/{ShapeId}")]
        public IActionResult DeleteShape(string ShapeId)
        {
            try
            {
                var existingShape = _shapeService.GetShapeById(ShapeId);
                if (existingShape == null)
                {
                    return NotFound("Không tìm thấy hồ");
                }

                bool pointsDeleted = _pointOfShapeService.DeletePointOfShapeByShapeID(ShapeId);
                bool shapeDeleted = _shapeService.DeleteShape(ShapeId);

                if (pointsDeleted || shapeDeleted)
                {
                    return Ok("Xóa hồ thành công");
                }
                else
                {
                    return BadRequest("Không có dữ liệu nào được xóa");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
    }
}

using FengShuiKoi_BO;
using FengShuiKoi_Services;
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

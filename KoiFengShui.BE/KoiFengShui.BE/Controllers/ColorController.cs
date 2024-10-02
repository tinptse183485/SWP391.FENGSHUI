using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
 

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
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
    }
}

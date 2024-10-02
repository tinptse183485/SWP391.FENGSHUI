using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
      
        private readonly IDirectionService _directionService;
        

        public DirectionController(IDirectionService directionService)
        {
            _directionService = directionService;
           

        }
        [HttpGet("GetAllDirection")]
        public IActionResult GetAllDirection()
        {
            List<Direction> listDirection = new List<Direction>();

            try
            {

                listDirection = _directionService.GetDirections();

                return Ok(listDirection);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

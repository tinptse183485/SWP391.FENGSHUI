using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ElementMutualismController : ControllerBase
	{
		private readonly IElementService _elementService;

		public ElementMutualismController( IElementService elementService)
		{
			_elementService = elementService;
		}

		[HttpGet("element")]
		public IActionResult GetElementByBirthYear(int birthYear)
		{
			try
			{
				var element = _elementService.GetElementByBirthYear(birthYear);
				return Ok(element);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
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
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}

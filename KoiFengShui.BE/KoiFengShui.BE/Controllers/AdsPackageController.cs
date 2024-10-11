using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdsPackageController : ControllerBase
	{
		private readonly IAdsPackageService _adsPackageService;

		public AdsPackageController(IAdsPackageService adsPackageService)
		{
			_adsPackageService = adsPackageService;
		}

		[HttpGet("GetAllAdsPackage")]
		public IActionResult GetAllAdsPackage()
		{
			List<AdsPackage> list = new List<AdsPackage>();

			try
			{
				list = _adsPackageService.GetAdsPackages();
				if (list.Count != 0)
				{
					return Ok(list);
				}
				else
				{
					return BadRequest("Không có AdsPackage nào");
				}

			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
		[HttpPost("AddAdsPackage")]
		public IActionResult AddAdsPackage(AdsPackage adsPackage)
		{
			try
			{
				if (adsPackage == null)
				{
					return BadRequest("AdsPackage is null.");
				}
				bool isSuccess = _adsPackageService.AddAdsPackage(adsPackage);
				if (isSuccess)
				{
					return Ok("Thêm AdsPackage thành công");
				}
				else
				{
					return BadRequest("Thêm AdsPackage thất bại");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}

using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IAdsPackageService _adsPackageService;
		private readonly IMemberService _memberService;
		private readonly IAdvertisementService _advertisementService;
		private readonly IKoiVarietyService _koiVarietyService;

		public DashboardController(IAdsPackageService adsPackageService, IMemberService memberService, IAdvertisementService advertisementService, IKoiVarietyService koiVarietyService)
		{
			_adsPackageService = adsPackageService;
			_memberService = memberService;
			_advertisementService = advertisementService;
			_koiVarietyService = koiVarietyService;
		}
		[HttpGet("RevenueByPackage")]
		public IActionResult GetRevenueByPackage()
		{
			try
			{
				var RevenueByPackage = _adsPackageService.GetRevenueByPackage();
				if (RevenueByPackage == null || !RevenueByPackage.Any())
				{
					return NotFound("Không tìm thấy dữ liệu doanh thu");
				}
				return Ok(RevenueByPackage);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
			}
		}
		[HttpGet("UsersByAgeGroup")]
		public IActionResult GetUsersByAgeGroup()
		{
			try
			{
				var usersByAgeGroup = _memberService.GetUsersByAgeGroup();
				if (usersByAgeGroup == null)
				{
					return NotFound("Không tìm thấy user");
				}
				return Ok(usersByAgeGroup);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
			}
		}
		//[HttpGet("PostsByPackage")]
		//public IActionResult GetPostsByPackage()
		//{
		//	try
		//	{
		//		var postsByPackage = _advertisementService.GetPostCountByPackage();
		//		return Ok(postsByPackage);
		//	}
		//	catch (Exception ex)
		//	{
		//		return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
		//	}
		//}

		//[HttpGet("KoiByElement")]
		//public IActionResult GetKoiByElement()
		//{
		//	try
		//	{
		//		var koiByElement = _koiVarietyService.GetKoiCountByElement();
		//		return Ok(koiByElement);
		//	}
		//	catch (Exception ex)
		//	{
		//		return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
		//	}
		//}
	}
}

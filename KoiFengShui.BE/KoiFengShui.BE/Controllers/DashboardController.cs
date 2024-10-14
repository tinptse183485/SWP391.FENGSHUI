using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetRevenueByPackage()
        {
            try
            {
                var RevenueByPackage = await _adsPackageService.GetRevenueByPackage();
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
        public async Task<IActionResult> GetUsersByAgeGroup()
        {
            try
            {
                var usersByAgeGroup = await _memberService.GetUsersByAgeGroup();
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
    }
}
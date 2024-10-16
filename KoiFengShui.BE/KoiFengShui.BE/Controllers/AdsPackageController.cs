using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllAdsPackage()
        {
            try
            {
                var list = await _adsPackageService.GetAdsPackages();
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
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPost("AddAdsPackage")]
        public async Task<IActionResult> AddAdsPackage(AdsPackage adsPackage)
        {
            try
            {
                if (adsPackage == null)
                {
                    return BadRequest("AdsPackage is null.");
                }
                bool isSuccess = await _adsPackageService.AddAdsPackage(adsPackage);
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
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        [HttpGet("GetTotalRevenueByMonth")]
        public async Task<IActionResult> GetTotalRevenueByMonth(int year, int month)
        {
            try
            {
                var GetTotal = await _adsPackageService.GetTotalRevenueByMonth(year, month);
                return Ok(GetTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }
    }
}
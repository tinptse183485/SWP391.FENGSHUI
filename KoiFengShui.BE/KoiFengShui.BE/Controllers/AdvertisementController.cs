﻿using FengShuiKoi_BO;
using FengShuiKoi_Services;
using KoiFengShui.BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IPackageService _packageService;

        private readonly IElementService _elementService;
        private readonly IAccountService _accountService;
        private readonly IAdsPackageService _adsPackageService;
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementController(IPackageService packageService, IAdvertisementService advertisementService, IAdsPackageService adsPackageService, IAccountService accountService, IElementService elementService)
        {
            _packageService = packageService;
            _adsPackageService = adsPackageService;
            _advertisementService = advertisementService;
            _accountService = accountService;
            _elementService = elementService;
        }

        [HttpGet("GetAllAdvertisement")]
        public IActionResult GetAllAdvertisement()
        {
            List<Advertisement> listAdvertisement = new List<Advertisement>();

            try
            {
                listAdvertisement = _advertisementService.GetAdvertisements();


                return Ok(listAdvertisement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllAdvertisementByAdId")]
        public IActionResult GetAllAdvertisementByAdId(string adId)
        {
            

            try
            {
                var advertisements = _advertisementService.GetAdvertisementByAdID(adId);
                if(advertisements == null)
                {
                    return BadRequest("Không tìm thấy quảng cáo");
                }
                
                return Ok(advertisements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        private string GenerateUniqueAdId()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1000); // Tạo số ngẫu nhiên từ 0 đến 999
            return $"AD{randomNumber:D3}";
        }
        [HttpPost("AddAdvertisement")]
        public IActionResult AddAdvertisement(AdvertisementDTO advertisementDto)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(advertisementDto.Rank) || string.IsNullOrWhiteSpace(advertisementDto.AdId))
                {
                    return BadRequest("Rank, Member ID, and Status are required.");
                }
                if (_packageService.GetPackageByRank(advertisementDto.Rank) == null)
                {
                    return BadRequest(" There not have the package. ");

                }
                if (_accountService.GetAccountByUserID(advertisementDto.UserId) == null)
                {
                    return BadRequest(" Member ID are not found. ");

                }

                // Generate unique AdId
                string adId = GenerateUniqueAdId();
                int attempts = 0;
                const int maxAttempts = 10;

                while (_advertisementService.GetAdvertisementByAdID(adId) != null && attempts < maxAttempts)
                {
                    adId = GenerateUniqueAdId();
                    attempts++;
                }

                if (attempts == maxAttempts)
                {
                    return StatusCode(500, "Failed to generate a unique advertisement ID. Please try again.");
                }

                // Create new Advertisement object
                var advertisement = new Advertisement
                {
                    AdId = adId,
                    Heading = advertisementDto.Heading?.Trim(),
                    Image = advertisementDto.Image?.Trim(),
                    Link = advertisementDto.Link?.Trim(),
                    UserId = advertisementDto.UserId,
                    Rank = advertisementDto.Rank,
                    ElementId = "None",
                    status = advertisementDto.status,
                };

                // Add advertisement
                bool result = _advertisementService.AddAdvertisement(advertisement);

                if (result)
                {
                    return Ok(new { Message = "Tạo quảng cáo thành công", AdId = adId });
                }
                else
                {
                    return BadRequest("Tạo quảng cáo thất bại");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred while adding advertisement: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        [HttpPut("UpdateAdvertisement")]
        public IActionResult UpdatePackage(AdvertisementDTO advertisement)
        {
            try
            {
                var existingAdvertisement = _advertisementService.GetAdvertisementByAdID(advertisement.AdId);
                if (existingAdvertisement == null)
                {
                    return NotFound("Không tìm thấy quảng cáo");
                }
                if (_elementService.GetElementAndMutualism(advertisement.ElementId) == null)
                {
                    return BadRequest("Không tìm thấy nguyên tố phù hợp ");
                }

                existingAdvertisement.ElementId = advertisement.ElementId;
                existingAdvertisement.status = advertisement.status;


                bool result = _advertisementService.UpdateAdvertisement(existingAdvertisement.AdId);
                if (result)
                {
                    return Ok("Cập nhật gói thành công");
                }
                else
                {
                    return BadRequest("Cập nhật gói thất bại");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteAdvertisement/{adId}")]
        public IActionResult DeleteAdvertisement(string adId)
        {
            try
            {

                var existingAdvertisement = _advertisementService.GetAdvertisementByAdID(adId);
                if (existingAdvertisement == null)
                {
                    return NotFound("Không tìm thấy quảng cáo");
                }

                bool result = _advertisementService.DeleteAdvertisement(adId);
                if (result)
                {
                    return Ok("Xóa quảng cáo thành công");
                }
                else
                {
                    return BadRequest("Xóa quảng cáo thất bại");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
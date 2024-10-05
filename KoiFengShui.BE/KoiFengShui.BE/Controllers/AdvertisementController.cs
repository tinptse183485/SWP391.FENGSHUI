using FengShuiKoi_BO;
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

      
        private readonly IAccountService _accountService;
        private readonly IAdsPackageService _adsPackageService;
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementController(IPackageService packageService, IAdvertisementService advertisementService, IAdsPackageService adsPackageService, IAccountService accountService)
        {
            _packageService = packageService;
            _adsPackageService = adsPackageService;
            _advertisementService = advertisementService;
            _accountService = accountService;
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
        private string GenerateUniqueAdId()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1000); // Tạo số ngẫu nhiên từ 0 đến 999
            return $"AD{randomNumber:D3}";
        }
        [HttpPost("AddAdvertisement")]
        public IActionResult AddAdvertisement(AdvertisementDTO advertisementDto, string rank, string memberID, string status)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(rank) || string.IsNullOrWhiteSpace(memberID) || string.IsNullOrWhiteSpace(status))
                {
                    return BadRequest("Rank, Member ID, and Status are required.");
                }
                if (_packageService.GetPackageByRank(rank) == null)
                {
                    return BadRequest(" There not have the package. ");

                }
                if (_accountService.GetAccountByUserID(memberID) == null)
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
                    UserId = memberID,
                    Rank = rank,
                    ElementId = "None",
                    status = status
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
    }
}

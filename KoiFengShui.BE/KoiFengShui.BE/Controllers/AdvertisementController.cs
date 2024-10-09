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
        [HttpGet("GetAdvertisementByAdId")]
        public IActionResult GetAdvertisementByAdId(string adId)
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

		[HttpGet("GetAdvertisementByUserIDandStatus")]
		public IActionResult GetAdvertisementByUserIDandStatus(string userID,string status)
		{
			try
			{
				var advertisements = _advertisementService.GetAdvertisementByUserIdAndStatus(userID, status);
				if (advertisements == null)
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
        [HttpGet("GetAdvertisementByStatusAdmin")]
        public IActionResult GetAdvertisementByStatusAdmin(string status)
        {
            try
            {
                var advertisements = _advertisementService.GetAdvertisementStatus( status);
                if (advertisements == null)
                {
                    return BadRequest("Không tìm thấy quảng cáo có trạng thái" + status);
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
        [HttpPost("AddAdvertisementDraft")]
        public IActionResult AddAdvertisementDraft(AdvertisementDTO advertisementDto)
        {
            try
            {
                // Validate input
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
                    ElementId = "None",
                    Status = "Draft",
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
        [HttpPut("UpdateDaftAdvertisement")]
        public IActionResult UpdateDaftAdvertisement(AdvertisementDTO advertisement)
        {
            try
            {
                var existingAdvertisement = _advertisementService.GetAdvertisementByAdID(advertisement.AdId);
                if (existingAdvertisement == null)
                {
                    return NotFound("Không tìm thấy quảng cáo");
                }
                existingAdvertisement.Image = advertisement.Image;
                existingAdvertisement.Heading = advertisement.Heading;
                existingAdvertisement.Link = advertisement.Link;
                bool result1 = _advertisementService.UpdateAdvertisement(existingAdvertisement.AdId);
               
               

                if (result1)
                {
                    return Ok("Cập nhật Draft thành công");
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

        [HttpPut("UpdateAdvertisement")]
        public IActionResult UpdateAdvertisement(AdvertisementDTO advertisement,string Rank,string Status,DateTime startDate,int quantity,float total)
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
                existingAdvertisement.Status = Status;
                bool result1 = _advertisementService.UpdateAdvertisement(existingAdvertisement.AdId);
                AdsPackage adsPackage = _adsPackageService.GetAdsPackageByAdIDRank(advertisement.AdId, Rank);
                Package package = _packageService.GetPackageByRank(Rank);
                adsPackage.StartDate = startDate;
                adsPackage.ExpiredDate = startDate.AddDays(package.Duration);
				adsPackage.Quantity = quantity;
                adsPackage.Total = total;
                bool result2 = _adsPackageService.UpdateAdsPackage(adsPackage);

				if (result1)
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

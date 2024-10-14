using FengShuiKoi_BO;
using FengShuiKoi_Services;
using KoiFengShui.BE.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllAdvertisement()
        {
            try
            {
                var listAdvertisement = await _advertisementService.GetAdvertisements();
                return Ok(listAdvertisement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("GetAdvertisementByAdId")]
        public async Task<IActionResult> GetAdvertisementByAdId(string adId)
        {
            try
            {
                var advertisements = await _advertisementService.GetAdvertisementByAdID(adId);
                if (advertisements == null)
                {
                    return NotFound("Không tìm thấy quảng cáo");
                }
                return Ok(advertisements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }


		[HttpGet("GetAdvertisementByUserId")]
		public IActionResult GetAdvertisementByUserId(string  UserId)
		{
			try
			{
				var advertisements = _advertisementService.GetAdvertisementByUserID(UserId);
				if (advertisements == null)
				{
					return NotFound("Người dùng chưa có quảng cáo");
				}
				return Ok(advertisements);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi server: {ex.Message}");
			}
		}
 [HttpGet("CheckAdIdExist")]
        public async Task<IActionResult> CheckAdIdExist(string adId)

        {
            try
            {
                var advertise = await _advertisementService.GetAdvertisementByAdID(adId);
                return Ok(advertise != null ? "True" : "False");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("GetAdvertisementByUserIDandStatus")]
        public async Task<IActionResult> GetAdvertisementByUserIDandStatus(string userID, string status)
        {
            try
            {
                var advertisements = await _advertisementService.GetAdvertisementByUserIdAndStatus(userID, status);
                if (advertisements == null)
                {
                    return BadRequest("Không tìm thấy quảng cáo");
                }
                return Ok(advertisements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("GetAdvertisementByRank")]
        public async Task<IActionResult> GetAdvertisementByRank(string rank)
        {
            List<Advertisement> listAd = new List<Advertisement>();
            try
            {
                List<AdsPackage> list = await _adsPackageService.GetListAdsPackageByRank(rank);
                foreach (AdsPackage ad in list)
                {
                    Advertisement ads = _advertisementService.GetAdvertisementByAdID(ad.AdId);
                    if (ad.StartDate <= DateTime.Now && ad.ExpiredDate >= DateTime.Now && ads.Status.Equals("Approved"))
                    {

                        var advertisement = await _advertisementService.GetAdvertisementByAdID(ad.AdId);
                        if (advertisement != null)
                        {
                            listAd.Add(advertisement);
                        }

                    }
                }
                if (listAd.Count == 0)
                {
                    return BadRequest("Không tìm thấy quảng cáo");
                }
                return Ok(listAd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("GenerateAdId")]
        public async Task<IActionResult> GenerateAdId(string AdId)

        {
            try
            {
                // Kiểm tra định dạng AdId
                string pattern = @"^AD\d{3}$";
                if (!Regex.IsMatch(AdId, pattern))
                {
                    string adId = GenerateUniqueAdId();
                    int attempts = 0;
                    const int maxAttempts = 10;

                    while (await _advertisementService.GetAdvertisementByAdID(adId) != null && attempts < maxAttempts)
                    {
                        adId = GenerateUniqueAdId();
                        attempts++;
                    }
                    if (attempts == maxAttempts)
                    {
                        return StatusCode(500, "Không tạo được ID quảng cáo. Vui lòng thử lại.");
                    }
                    return Ok(adId);
                }
                return Ok(AdId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        private string GenerateUniqueAdId()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1000); // Tạo số ngẫu nhiên từ 0 đến 999
            return $"AD{randomNumber:D3}";
        }

        [HttpGet("GetAdvertisementByStatusAdmin")]
        public async Task<IActionResult> GetAdvertisementByStatusAdmin(string status)
        {
            try
            {
                var advertisements = await _advertisementService.GetAdvertisementStatus(status);
                if (advertisements == null)
                {
                    return BadRequest("Không tìm thấy quảng cáo có trạng thái" + status);
                }
                return Ok(advertisements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPost("SaveAdvertisementDraft")]
        public async Task<IActionResult> SaveAdvertisementDraft(AdvertisementDTO advertisementDto)
        {
            try
            {
                // Kiểm tra xem quảng cáo đã tồn tại chưa
                Advertisement existingAdvertisement = new Advertisement();
                if (Regex.IsMatch(advertisementDto.AdId, @"^AD\d{3}$"))
                {
                    existingAdvertisement = await _advertisementService.GetAdvertisementByAdID(advertisementDto.AdId);
                }

                if (existingAdvertisement == null)
                {
                    // Tạo mới quảng cáo
                    if (await _accountService.GetAccountByUserID(advertisementDto.UserId) == null)
                    {
                        return BadRequest("Không tìm thấy ID của người dùng.");
                    }

                    string adId = GenerateUniqueAdId();
                    int attempts = 0;
                    const int maxAttempts = 10;

                    while (await _advertisementService.GetAdvertisementByAdID(adId) != null && attempts < maxAttempts)
                    {
                        adId = GenerateUniqueAdId();
                        attempts++;
                    }

                    if (attempts == maxAttempts)
                    {
                        return StatusCode(500, "Không tạo được ID quảng cáo. Vui lòng thử lại.");
                    }

                    var newAdvertisement = new Advertisement
                    {
                        AdId = adId,
                        Heading = advertisementDto.Heading?.Trim(),
                        Image = advertisementDto.Image?.Trim(),
                        Link = advertisementDto.Link?.Trim(),
                        UserId = advertisementDto.UserId,
                        ElementId = advertisementDto.ElementId,
                        Status = "Draft",
                    };

                    bool result = await _advertisementService.AddAdvertisement(newAdvertisement);

                    if (result)
                    {
                        return Ok(new { Message = "Tạo bản nháp quảng cáo thành công", AdId = adId });
                    }
                    else
                    {
                        return BadRequest("Tạo bản nháp quảng cáo thất bại");
                    }
                }
                else
                {
                    // Cập nhật quảng cáo hiện có
                    existingAdvertisement.Heading = advertisementDto.Heading?.Trim();
                    existingAdvertisement.Image = advertisementDto.Image?.Trim();
                    existingAdvertisement.Link = advertisementDto.Link?.Trim();
                    existingAdvertisement.ElementId = advertisementDto.ElementId;
                    existingAdvertisement.Status = "Draft";

                    bool result = await _advertisementService.UpdateAdvertisement(existingAdvertisement);
                    if (result)
                    {
                        return Ok("Cập nhật bản nháp quảng cáo thành công");
                    }
                    else
                    {
                        return BadRequest("Cập nhật bản nháp quảng cáo thất bại");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPost("AddAdvertisementDraft")]
        public async Task<IActionResult> AddAdvertisementDraft(AdvertisementDTO advertisementDto)
        {
            try
            {
                // Validate input
                if (await _accountService.GetAccountByUserID(advertisementDto.UserId) == null)
                {
                    return BadRequest(" Không tìm thấy ID của người dùng. ");
                }
                // Generate unique AdId
                string adId = GenerateUniqueAdId();
                int attempts = 0;
                const int maxAttempts = 10;

                while (await _advertisementService.GetAdvertisementByAdID(adId) != null && attempts < maxAttempts)
                {
                    adId = GenerateUniqueAdId();
                    attempts++;
                }

                if (attempts == maxAttempts)
                {
                    return StatusCode(500, "Không tạo được ID quảng cáo. Vui lòng thử lại.");
                }
                // Create new Advertisement object
                var advertisement = new Advertisement
                {
                    AdId = adId,
                    Heading = advertisementDto.Heading?.Trim(),
                    Image = advertisementDto.Image?.Trim(),
                    Link = advertisementDto.Link?.Trim(),
                    UserId = advertisementDto.UserId,
                    ElementId = advertisementDto.ElementId,
                    Status = "Draft",
                };
                // Add advertisement
                bool result = await _advertisementService.AddAdvertisement(advertisement);

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
                return StatusCode(500, "Lỗi server. Vui lòng thử lại.");
            }
        }


        [HttpPut("UpdateAdvertisementStatus")]
        public async Task<IActionResult> UpdateAdvertisementStatus(string adId, string status)

        {
            try
            {
                var advertise = await _advertisementService.GetAdvertisementByAdID(adId);
                if (advertise == null)
                {
                    return BadRequest("Không tìm thấy bài quảng cáo");
                }
                else
                {
                    advertise.ElementId = elementID;
                    advertise.Status = status;
                    bool check = await _advertisementService.UpdateAdvertisement(advertise);
                    if (check)
                    {
                        return Ok("Cập nhật thành công");
                    }
                    else
                    {
                        return BadRequest("Cập nhật thất bại");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }


        [HttpPut("UpdateDaftAdvertisement")]
        public async Task<IActionResult> UpdateDaftAdvertisement(AdvertisementDTO advertisement)
        {
            try
            {
                var existingAdvertisement = await _advertisementService.GetAdvertisementByAdID(advertisement.AdId);
                if (existingAdvertisement == null)
                {
                    return NotFound("Không tìm thấy quảng cáo");
                }
                existingAdvertisement.Image = advertisement.Image;
                existingAdvertisement.Heading = advertisement.Heading;
                existingAdvertisement.Link = advertisement.Link;
                existingAdvertisement.Status = "Draft";
                bool result1 = await _advertisementService.UpdateAdvertisement(existingAdvertisement);
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
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        

		[HttpPut("UpdateAdvertisementStatus")]
		public IActionResult UpdateAdvertisementStatus(string adId, string status)
		{
			try
			{
				var advertise = _advertisementService.GetAdvertisementByAdID(adId);
				if (advertise == null)
				{
					return BadRequest("Không tìm thấy bài quảng cáo");
				}
				else
				{
					advertise.Status = status;
					bool check = _advertisementService.UpdateAdvertisement(advertise);
					if (check)
					{
						return Ok("Cập nhật thành công");
					}
					else
					{
						return BadRequest("Cập nhật thất bại");
					}
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi server: {ex.Message}");
			}
		}


	[HttpPut("UpdateAdvertisement")]
        public async Task<IActionResult> UpdateAdvertisement(AdvertisementDTO advertisement, string Rank, string Status, DateTime startDate, int quantity, float total)

        {
            try
            {
                var existingAdvertisement = await _advertisementService.GetAdvertisementByAdID(advertisement.AdId);
                if (existingAdvertisement == null)
                {
                    return NotFound("Không tìm thấy quảng cáo");
                }
                if (await _elementService.GetElementAndMutualism(advertisement.ElementId) == null)
                {
                    return BadRequest("Không tìm thấy nguyên tố phù hợp ");
                }
                existingAdvertisement.ElementId = advertisement.ElementId;
                existingAdvertisement.Status = Status;
                bool result1 = await _advertisementService.UpdateAdvertisement(existingAdvertisement);
                AdsPackage adsPackage = await _adsPackageService.GetAdsPackageByAdIDRank(advertisement.AdId, Rank);
                Package package = await _packageService.GetPackageByRank(Rank);
                if (adsPackage == null)
                {
                    AdsPackage newads = new AdsPackage();
                    newads.AdId = advertisement.AdId;
                    newads.Rank = Rank;
                    newads.StartDate = startDate;
                    newads.ExpiredDate = startDate.AddDays(package.Duration * quantity);
                    newads.Quantity = quantity;
                    newads.Total = total;
                    bool result2 = await _adsPackageService.AddAdsPackage(newads);
                    if (result2)
                    {
                        return Ok("Thêm quảng cáo thành công");
                    }
                }
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
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPost("CreateAdvertisement")]
        public async Task<IActionResult> CreateAdvertisement(AdvertisementDTO advertisement, string Rank, DateTime startDate, int quantity, float total)
        {
            try
            {
                Advertisement existingAdvertisement = null;
                bool isNewAd = !Regex.IsMatch(advertisement.AdId ?? "", @"^AD\d{3}$");

                if (!isNewAd)
                {
                    existingAdvertisement = await _advertisementService.GetAdvertisementByAdID(advertisement.AdId);
                    if (existingAdvertisement == null)
                    {
                        return NotFound("Không tìm thấy bản nháp quảng cáo");
                    }
                }

                if (await _elementService.GetElementAndMutualism(advertisement.ElementId) == null)
                {
                    return BadRequest("Không tìm thấy nguyên tố phù hợp");
                }

                Advertisement newAd;
                if (isNewAd)
                {
                    string newAdId = GenerateUniqueAdId();
                    newAd = new Advertisement
                    {
                        AdId = newAdId,
                        UserId = advertisement.UserId,
                        Heading = advertisement.Heading,
                        Image = advertisement.Image,
                        Link = advertisement.Link,
                        ElementId = advertisement.ElementId,
                        Status = "Pending"
                    };
                }
                else
                {
                    newAd = existingAdvertisement;
                    newAd.Heading = advertisement.Heading;
                    newAd.Image = advertisement.Image;
                    newAd.Link = advertisement.Link;
                    newAd.ElementId = advertisement.ElementId;
                    newAd.Status = "Pending";
                }

                bool result;
                if (isNewAd)
                {
                    result = await _advertisementService.AddAdvertisement(newAd);
                }
                else
                {
                    result = await _advertisementService.UpdateAdvertisement(newAd);
                }

                if (!result)
                {
                    return BadRequest("Tạo quảng cáo thất bại");
                }

                Package package = await _packageService.GetPackageByRank(Rank);
                if (package == null)
                {
                    return BadRequest("Không tìm thấy gói quảng cáo phù hợp");
                }

                AdsPackage newAdsPackage = new AdsPackage
                {
                    AdId = newAd.AdId,
                    Rank = Rank,
                    StartDate = startDate,
                    ExpiredDate = startDate.AddDays(package.Duration * quantity),
                    Quantity = quantity,
                    Total = total
                };

                bool adsPackageResult = await _adsPackageService.AddAdsPackage(newAdsPackage);
                if (!adsPackageResult)
                {
                    return BadRequest("Thêm gói quảng cáo thất bại");
                }

                return Ok(new { Message = "Tạo quảng cáo thành công", AdId = newAd.AdId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpDelete("DeleteAdvertisement/{adId}")]
        public async Task<IActionResult> DeleteAdvertisement(string adId)
        {
            try
            {
                var existingAdvertisement = await _advertisementService.GetAdvertisementByAdID(adId);
                if (existingAdvertisement == null)
                {
                    return NotFound("Không tìm thấy quảng cáo");
                }

                bool result = await _advertisementService.DeleteAdvertisement(adId);
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
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
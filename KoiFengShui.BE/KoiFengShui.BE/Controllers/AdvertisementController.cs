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
        public async Task<IActionResult> GetAdvertisementByUserId(string UserId)
        {
            try
            {
                var advertisements = await _advertisementService.GetAdvertisementByUserID(UserId);
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
                    Advertisement ads = await _advertisementService.GetAdvertisementByAdID(ad.AdId);
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
                if (string.IsNullOrEmpty(advertisementDto.UserId))
                {
                    return BadRequest("ID người dùng là bắt buộc.");
                }

                if (await _accountService.GetAccountByUserID(advertisementDto.UserId) == null)
                {
                    return BadRequest("Không tìm thấy ID của người dùng.");
                }

                string adId = await GenerateOrValidateAdId(advertisementDto.AdId);
                if (adId == null)
                {
                    return StatusCode(500, "Không tạo được ID quảng cáo. Vui lòng thử lại.");
                }

                Advertisement advertisement = await _advertisementService.GetAdvertisementByAdID(adId);
                bool isNewAdvertisement = advertisement == null;

                if (isNewAdvertisement)
                {
                    advertisement = new Advertisement
                    {
                        AdId = adId,
                        UserId = advertisementDto.UserId,
                        Status = "Draft"
                    };
                }

                // Cập nhật thuộc tính quảng cáo
                advertisement.Heading = advertisementDto.Heading?.Trim();
                advertisement.Image = advertisementDto.Image?.Trim();
                advertisement.Link = advertisementDto.Link?.Trim();
                advertisement.ElementId = advertisementDto.ElementId;

                bool result = isNewAdvertisement
                    ? await _advertisementService.AddAdvertisement(advertisement)
                    : await _advertisementService.UpdateAdvertisement(advertisement);

                if (result)
                {
                    return Ok(new { Message = isNewAdvertisement ? "Tạo bản nháp quảng cáo thành công" : "Cập nhật bản nháp quảng cáo thành công", AdId = advertisement.AdId });
                }
                else
                {
                    return BadRequest(isNewAdvertisement ? "Tạo bản nháp quảng cáo thất bại" : "Cập nhật bản nháp quảng cáo thất bại");
                }
            }
            catch (Exception ex)
            {
                // Ghi log chi tiết lỗi ở đây
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu. Vui lòng thử lại sau.");
            }
        }

        private async Task<string> GenerateOrValidateAdId(string adId)
        {
            string pattern = @"^AD\d{3}$";
            if (Regex.IsMatch(adId, pattern))
            {
                return adId;
            }

            const int maxAttempts = 10;
            for (int i = 0; i < maxAttempts; i++)
            {
                string newAdId = GenerateUniqueAdId();
                if (await _advertisementService.GetAdvertisementByAdID(newAdId) == null)
                {
                    return newAdId;
                }
            }
            return null;
        }

        private string GenerateUniqueAdId()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1000); // Tạo số ngẫu nhiên từ 0 đến 999
            return $"AD{randomNumber:D3}";
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

        [HttpPut("ApproveAdvertisement")]
        public async Task<IActionResult> ApproveAdvertisement(string adId, string elementID, string status)
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






        [HttpPut("UpdateAdvertisement")]
        public async Task<IActionResult> UpdateAdvertisement(AdvertisementDTO advertisement, string Rank, string Status, DateTime startDate, DateTime CreateAt, int quantity, float total)

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
                AdsPackage adsPackage = await _adsPackageService.GetAdsPackageByAdIDRankTime(advertisement.AdId, Rank, CreateAt);
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
                    newads.CreateAt = CreateAt;
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
        public async Task<IActionResult> CreateAdvertisement(AdvertisementDTO advertisement, string Rank, DateTime startDate, DateTime CreateAt, int quantity, float total)
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
                    Total = total,
                    CreateAt = CreateAt
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
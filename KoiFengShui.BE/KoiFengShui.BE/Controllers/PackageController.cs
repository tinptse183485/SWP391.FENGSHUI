using FengShuiKoi_BO;
using FengShuiKoi_Services;
using KoiFengShui.BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        private readonly IAdsPackageService _adsPackageService;
        private readonly IAdvertisementService _advertisementService;
        public PackageController(IPackageService packageService,IAdvertisementService advertisementService, IAdsPackageService adsPackageService)
        {
            _packageService = packageService;
            _adsPackageService = adsPackageService;
            _advertisementService = advertisementService;
        }
        
        [HttpGet("GetAllPackage")]
        public IActionResult GetAllPackage()
        {
            List<Package> listPackage = new List<Package>();

            try
            {
                listPackage = _packageService.GetPackages();


                return Ok(listPackage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddPackage")]
        public IActionResult AddPackage(PackageDTO packageDto)
        {
            try
            {
                if (_packageService.GetPackageByRank(packageDto.Rank) != null)
                {
                    return BadRequest("Gói này đã tồn tại");
                }

                var package = new Package
                {
                    Rank = packageDto.Rank,
                    Duration = packageDto.Duration,
                    Description = packageDto.Description,
                    Price = packageDto.Price
                };

                bool result = _packageService.AddPackage(package);

                if (result)
                {
                    return Ok("Tạo gói thành công");
                }
                else
                {
                    return BadRequest("Tạo gói thất bại");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdatePackage")]
        public IActionResult UpdatePackage(PackageDTO packageDto)
        {
            try
            {
                var existingPackage = _packageService.GetPackageByRank(packageDto.Rank);
                if (existingPackage == null)
                {
                    return NotFound("Không tìm thấy gói phù hợp");
                }

                
                existingPackage.Duration = packageDto.Duration;
                existingPackage.Description = packageDto.Description;
                existingPackage.Price = packageDto.Price;

                bool result = _packageService.UpdatePackage(existingPackage);
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
        private IActionResult CheckPackageExpiration(List<AdsPackage> listAdsPackage)
        {
            DateTime currentTime = DateTime.Now;

            foreach (var ads in listAdsPackage)
            {
                if (ads.ExpiredDate > currentTime)
                {
                    return BadRequest("Failed to delete package because it contains ads that have not expired yet");
                }
            }

            return null;
        }
        [HttpDelete("DeletePackage/{rank}")]
        public IActionResult DeletePackage(string rank)
        {
            try
            {
                List<AdsPackage> listAdsPackage = new List<AdsPackage>();
                List<Advertisement> listAdvertisements = _advertisementService.GetAdvertisementByRank(rank);

                foreach (var advertisement in listAdvertisements)
                {
                    AdsPackage adsPackage = _adsPackageService.GetAdsPackageByAdIDRank(advertisement.AdId, rank);
                    if (adsPackage != null)
                    {
                        listAdsPackage.Add(adsPackage); 
                    }   
                }
                IActionResult expirationCheck = CheckPackageExpiration(listAdsPackage);
                if (expirationCheck != null)
                {
                    return expirationCheck;
                }else
                {
                    foreach(var ads in listAdsPackage)
                    {
                        _adsPackageService.DeleteAdsPackage(ads.AdId, rank);
                    }
                    foreach (var advertise in listAdvertisements)
                    {
                        _advertisementService.DeleteAdvertisement(advertise.AdId);
                    }
                    bool result = _packageService.DeletePackage(rank);
                    if (result)
                    {
                        return Ok("Package deleted successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to delete package");
                    }
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

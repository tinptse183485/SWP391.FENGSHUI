﻿using FengShuiKoi_BO;
using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

using System.Xml.Linq;
using static KoiFengShui.BE.Controllers.ColorController;

namespace KoiFengShui.BE.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class KoiVarietyController : ControllerBase
    {
        private readonly IKoiVarietyService _koiVarietyService;
        private readonly IQuantityOfFishService _QuantityService;
        private readonly IElementService _elementService;
        private readonly IColorService _colorService;
        private readonly ITypeColorService _typeColorService;
        private readonly IElementColorService _elementColorService;


        public KoiVarietyController(IKoiVarietyService koiVarietyService, IQuantityOfFishService quantityService, IElementService elementService, IColorService colorService, ITypeColorService typeColorService, IElementColorService elementColorService)
        {
            _koiVarietyService = koiVarietyService;
            _QuantityService = quantityService;
            _elementService = elementService;
            _colorService = colorService;
            _typeColorService = typeColorService;
            _elementColorService = elementColorService;
        }
        [HttpGet("GetQuantityByDOB")]
        public IActionResult GetQuantityByElement(string dob)
        {
            int year = int.Parse(dob.Substring(0, 4));
            try
            {
                string element = _elementService.GetElementByBirthYear(year);
                var quantity = _QuantityService.getQuantityByElement(element);
                return Ok(quantity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpGet("GetListKoiByDOBOrder")]
        public IActionResult GetListKoiByDOBOrder(string dob)
        {
            List<KoiVariety> listKoi = new List<KoiVariety>();
            int year = int.Parse(dob.Substring(0, 4));
            try
            {
                string element = _elementService.GetElementByBirthYear(year);
                var mutual = _elementService.GetElementAndMutualism(element);
                var list1 = _koiVarietyService.GetKoiVarietiesByElemnet(mutual.Mutualism);
                var list2 = _koiVarietyService.GetKoiVarietiesByElemnet(element);
                listKoi.AddRange(list1);
                listKoi.AddRange(list2);
                return Ok(listKoi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpGet("GetListKoiByDOBOrderS1")]
        public IActionResult GetListKoiByDOBOrderS1(string dob)
        {
            int year = int.Parse(dob.Substring(0, 4));
            try
            {
                string element = _elementService.GetElementByBirthYear(year);
                var mutual = _elementService.GetElementAndMutualism(element);
                var list1 = _koiVarietyService.GetKoiVarietiesByElemnet(mutual.Mutualism);
                var list2 = _koiVarietyService.GetKoiVarietiesByElemnet(element);

                var allKoi = new List<KoiVariety>();
                allKoi.AddRange(list1);
                allKoi.AddRange(list2);

                var scoredKoi = new List<(KoiVariety Koi, double Score)>();

                foreach (var koi in allKoi)
                {
                    var koiColors = _typeColorService.GetColorsAndPercentages(koi.KoiType);
                    double totalScore = 0;

                    foreach (var color in koiColors)
                    {
                        var pointForColor = _elementColorService.GetPointElementColor(element, color.ColorId);
                        totalScore += pointForColor * color.Percentage;
                    }

                    scoredKoi.Add((koi, totalScore));
                }

                var sortedKoi = scoredKoi.OrderByDescending(k => k.Score).Select(k => k.Koi).ToList();

                return Ok(sortedKoi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }


        [HttpGet("GetAllKoi")]
        public IActionResult GetAllKoiVarieties()
        {
            List<KoiVariety> listKoi = new List<KoiVariety>();

            try
            {

                listKoi = _koiVarietyService.GetKoiVarieties();

                return Ok(listKoi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpGet("GetListKoiByColor")]
        public IActionResult GetListKoiByColor(string color)
        {
            List<KoiVariety> listKoi = new List<KoiVariety>();
            try
            {
                List<TypeColor> listTypeColor = _typeColorService.GetTypeByColor(color);

                foreach (TypeColor typeColor in listTypeColor)
                {

                    KoiVariety koiVariety = _koiVarietyService.GetKoiVarietyByType(typeColor.KoiType);


                    if (koiVariety != null)
                    {
                        listKoi.Add(koiVariety);
                    }
                }

                return Ok(listKoi); // Return the correct variable
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }

        }
        public class AddKoiTypeColor
        {
            public string KoiType { get; set; }
            public string Image { get; set; } = null!;
            public string Element { get; set; } = null!;
            public string Description { get; set; } = null!;
            public List<TypeColorOfFish> Colors { get; set; }
        }

        public class TypeColorOfFish
        {
            public string ColorId { get; set; }
            public double Percentage { get; set; }
        }
        private bool CheckDuplicateColors(List<TypeColorOfFish> colors)
        {
            var colorIds = new HashSet<string>();
            foreach (var color in colors)
            {
                if (!colorIds.Add(color.ColorId))
                {
                    return false;
                }
            }
            return true;
        }
        [HttpPost("AddKoiAndTypeColor")]
        public IActionResult AddKoiAndTypeColor([FromBody] AddKoiTypeColor koiFish)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(koiFish.KoiType))
                {
                    return BadRequest("Vui lòng nhập loại cá Koi!");
                }

                if (koiFish.Colors == null || koiFish.Colors.Count == 0)
                {
                    return BadRequest("Vui lòng nhập ít nhất một màu và tỷ lệ!");
                }

                if (_elementService.GetElementAndMutualism(koiFish.Element) == null)
                {
                    return BadRequest("Không có sinh mệnh này.");
                }
                if (_koiVarietyService.GetKoiVarietyByType(koiFish.KoiType) != null)
                {
                    return BadRequest("Loại cá Koi này đã tồn tại.");
                }
                var duplicateColorResult = CheckDuplicateColors(koiFish.Colors);
                if (!duplicateColorResult)
                {
                    return BadRequest("Chỉ được chọn 1 màu 1 lần");
                }
                double totalPercentage = 0;
                foreach (var color in koiFish.Colors)
                {
                    if (string.IsNullOrWhiteSpace(color.ColorId))
                    {
                        return BadRequest("Mã màu không được để trống.");
                    }

                    if (color.Percentage <= 0 || color.Percentage > 1)
                    {
                        return BadRequest($"Tỷ lệ cho màu {color.ColorId} phải lớn hơn 0 và không quá 1 (100%).");
                    }

                    totalPercentage += color.Percentage;

                    if (_colorService.GetColorById(color.ColorId) == null)
                    {
                        return BadRequest($"Màu {color.ColorId} không tồn tại trong hệ thống.");
                    }
                }

                if (Math.Abs(totalPercentage - 1) > 0.0001)
                {
                    return BadRequest($"Tổng tỷ lệ các màu phải bằng 1 (100%).");
                }


                var newKoiType = new KoiVariety
                {
                    KoiType = koiFish.KoiType,
                    Image = koiFish.Image,
                    Description = koiFish.Description,
                    Element = koiFish.Element,
                };

                bool koiTypeAdded = _koiVarietyService.AddKoiVariety(newKoiType);
                if (!koiTypeAdded)
                {
                    return BadRequest("Thêm loại cá Koi mới thất bại.");
                }

                var addedColors = new List<TypeColorOfFish>();
                foreach (var color in koiFish.Colors)
                {
                    var koiTypeColor = new TypeColor
                    {
                        KoiType = newKoiType.KoiType,
                        ColorId = color.ColorId,
                        Percentage = color.Percentage
                    };

                    bool colorAdded = _typeColorService.AddKoiTypeColor(koiTypeColor);
                    if (colorAdded)
                    {
                        addedColors.Add(color);
                    }
                    else
                    {

                        return BadRequest($"Thêm màu {color.ColorId} cho loại cá Koi thất bại.");
                    }
                }

                return Ok("Thêm loại cá Koi và màu sắc thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi máy chủ. Vui lòng thử lại sau.");
            }
        }
        [HttpPut("UpdateKoiAndTypeColor")]
        public IActionResult UpdateKoiAndTypeColor([FromBody] AddKoiTypeColor koiFish)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(koiFish.KoiType))
                {
                    return BadRequest("Vui lòng nhập loại cá Koi!");
                }

                var existingKoi = _koiVarietyService.GetKoiVarietyByType(koiFish.KoiType);
                if (existingKoi == null)
                {
                    return NotFound($"Không tìm thấy loại cá Koi: {koiFish.KoiType}");
                }

                if (koiFish.Colors == null || koiFish.Colors.Count == 0)
                {
                    return BadRequest("Vui lòng nhập ít nhất một màu và tỷ lệ!");
                }

                if (_elementService.GetElementAndMutualism(koiFish.Element) == null)
                {
                    return BadRequest("Không có sinh mệnh này.");
                }

                var duplicateColorResult = CheckDuplicateColors(koiFish.Colors);
                if (!duplicateColorResult)
                {
                    return BadRequest("Chỉ được chọn 1 màu 1 lần");
                }

                double totalPercentage = 0;
                foreach (var color in koiFish.Colors)
                {
                    if (string.IsNullOrWhiteSpace(color.ColorId))
                    {
                        return BadRequest("Mã màu không được để trống.");
                    }

                    if (color.Percentage <= 0 || color.Percentage > 1)
                    {
                        return BadRequest($"Tỷ lệ cho màu {color.ColorId} phải lớn hơn 0 và không quá 1 (100%).");
                    }

                    totalPercentage += color.Percentage;

                    if (_colorService.GetColorById(color.ColorId) == null)
                    {
                        return BadRequest($"Màu {color.ColorId} không tồn tại trong hệ thống.");
                    }
                }

                if (Math.Abs(totalPercentage - 1) > 0.0001)
                {
                    return BadRequest($"Tổng tỷ lệ các màu phải bằng 1 (100%).");
                }


                existingKoi.Image = koiFish.Image;
                existingKoi.Description = koiFish.Description;
                existingKoi.Element = koiFish.Element;

                bool koiTypeUpdated = _koiVarietyService.UpdateKoiVariety(existingKoi);
                if (!koiTypeUpdated)
                {
                    return BadRequest("Cập nhật loại cá Koi thất bại.");
                }


                _typeColorService.DeleteTypeColorByKoiType(koiFish.KoiType);


                var updatedColors = new List<TypeColorOfFish>();
                foreach (var color in koiFish.Colors)
                {
                    var koiTypeColor = new TypeColor
                    {
                        KoiType = existingKoi.KoiType,
                        ColorId = color.ColorId,
                        Percentage = color.Percentage
                    };

                    bool colorAdded = _typeColorService.AddKoiTypeColor(koiTypeColor);
                    if (colorAdded)
                    {
                        updatedColors.Add(color);
                    }
                    else
                    {
                        return BadRequest($"Cập nhật màu {color.ColorId} cho loại cá Koi thất bại.");
                    }
                }

                return Ok("Cập nhật loại cá Koi và màu sắc thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi máy chủ. Vui lòng thử lại sau.");
            }
        }
        [HttpDelete("DeleteKoiAndTypeColor/{KoiType}")]
        public IActionResult DeleteKoiAndTypeColor(string KoiType)
        {
            try
            {
                if (string.IsNullOrEmpty(KoiType))
                {
                    return BadRequest("Vui lòng điền loại cá");
                }

                var existingKoi = _koiVarietyService.GetKoiVarietyByType(KoiType);
                if (existingKoi == null)
                {
                    return NotFound("Không tìm thấy cá tương ứng.");
                }
                var existingTypeColorPoint = _typeColorService.GetTypeByKoiType(KoiType);
                if (existingTypeColorPoint?.Any() == true)
                {
                    _typeColorService.DeleteTypeColorByKoiType(KoiType);
                }


                bool result = _koiVarietyService.DeleteKoiVariety((KoiType));

                if (result)
                {
                    return Ok($"Xóa cá Koi {KoiType} và các màu liên quan thành công");
                }
                else
                {
                    return BadRequest($"Xóa cá Koi {KoiType} thất bại");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpDelete("DeleteKoiAndTypeColor/{KoiType}")]
        public IActionResult DeleteKoiAndTypeColor(string KoiType)
        {
            try
            {
                if (string.IsNullOrEmpty(KoiType))
                {
                    return BadRequest("Vui lòng điền loại cá");
                }

                var existingKoi = _koiVarietyService.GetKoiVarietyByType(KoiType);
                if (existingKoi == null)
                {
                    return NotFound("Không tìm thấy cá tương ứng.");
                }
                var existingTypeColorPoint = _typeColorService.GetTypeByKoiType(KoiType);
                if (existingTypeColorPoint?.Any() == true)
                {
                    _typeColorService.DeleteTypeColorByKoiType(KoiType);
                }


                bool result = _koiVarietyService.DeleteKoiVariety((KoiType));

                if (result)
                {
                    return Ok($"Xóa cá Koi {KoiType} và các màu liên quan thành công");
                }
                else
                {
                    return BadRequest($"Xóa cá Koi {KoiType} thất bại");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}

using FengShuiKoi_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeColorController : ControllerBase
    {
        private readonly IKoiVarietyService _koiVarietyService;
        private readonly IColorService _colorService;

        public TypeColorController(IKoiVarietyService koiVarietyService, IColorService colorService)
        {
            _koiVarietyService = koiVarietyService;
           _colorService = colorService;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortererApp.DTOs;
using UrlShortererApp.Services;

namespace UrlShortererApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {

        private readonly IUrlService _urlService;
        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost("GetShorterUrl")]
        public async Task<ActionResult<string>> GetShorterUrl(UrlDto urlDto)
        {
            try
            {
                return Ok(await _urlService.GetShorterUrl(urlDto));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

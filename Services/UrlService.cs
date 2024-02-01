
using UrlShorterer.Models;
using UrlShortererApp.DTOs;
using UrlShortererApp.Models;

namespace UrlShortererApp.Services
{
    public class UrlService : IUrlService
    {
        public const int numsOfChars = 8;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly Random _random = new();
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UrlService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));


        }
        public async Task<string> GenerateRandomCode()
        {

            var chars = new char[numsOfChars];

            while (true)
            {
                for(var i = 0; i< numsOfChars; i++)
                {
                    var randomIndex = _random.Next(Alphabet.Length - 1);
                    chars[i] = Alphabet[randomIndex];
                }
                var code = new string(chars);
                if (!_context.Urls.Any(s => s.Code == code))
                {
                    return code;
                }
            }

        }

        public async Task<string> GetShorterUrl(UrlDto urlDto)
        {
            var code = await GenerateRandomCode();

            var newUrl = new UrlModel
            {
                Id = Guid.NewGuid(),
                LongUrl = urlDto.LongUrl,
                ShortUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/{code}",
                Code = code
                
            };
            _context.Urls.Add(newUrl);
            await _context.SaveChangesAsync();

            return newUrl.ShortUrl.ToString();
        }

       
    }
}

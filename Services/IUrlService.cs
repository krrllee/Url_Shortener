using UrlShortererApp.DTOs;

namespace UrlShortererApp.Services
{
    public interface IUrlService
    {
        Task<string> GenerateRandomCode();
        Task<string> GetShorterUrl(UrlDto urlDto);

    }
}

using System.Threading.Tasks;
using UrlShortner.Api.Entities;

public interface IRepository
    {
        Task CreateAsync(LongUrl longUrl, string baseUrl);
        Task<string> GetLongUrl(string key);
    }
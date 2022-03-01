namespace UrlShortner.Api.Dtos
{
    public record LongUrlDto(string longUrl);
    public record ShortUrlDto(string id, string shortUrl);
}
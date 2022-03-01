using System;

namespace UrlShortner.Api.Entities
{
    public class LongUrl {
        public Guid id { get; set; }
        public string longUrl { get; set; }
        public string shortUrl { get; set; }
        public string shortUrlId { get; set; }
        public DateTimeOffset createdDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
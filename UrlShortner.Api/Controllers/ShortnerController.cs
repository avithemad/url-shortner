using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Api.Dtos;
using UrlShortner.Api.Entities;

namespace UrlShortner.Api.Controllers
{
    [ApiController]
    [Route("shortner")]
    public class ShortnerController : ControllerBase
    {
        private readonly IRepository repository;
        public ShortnerController(IRepository repository){
            this.repository = repository;
        }
        [HttpPost]
        public async Task<ActionResult<ShortUrlDto>> Post(LongUrlDto longUrl){
            var longUrlEntity = new LongUrl {
                longUrl = longUrl.longUrl,
                createdDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow
            };
            var baseUrl = $"{Request.Scheme}://{Request.Host}/";
            await repository.CreateAsync(longUrlEntity, baseUrl);

            return Created(longUrlEntity.shortUrl, longUrlEntity);
        }

        
    }
}
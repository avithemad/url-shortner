using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Api.Dtos;
using UrlShortner.Api.Entities;

namespace UrlShortner.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class RedirectController : ControllerBase
    {
        private readonly IRepository repository;
        public RedirectController(IRepository repository){
            this.repository = repository;
        }
        [HttpGet]
        [Route("{key}")]
        public async Task<ActionResult> redirect(string key){
            var longUrl = await this.repository.GetLongUrl(key);
            if (longUrl == null){
                return NotFound("Content not found.");
            }
            return Redirect(longUrl);
        }         
    }
}
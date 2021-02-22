using System;
using Blazify.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazify.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyAccountController
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SpotifyAccountController(ISpotifyAccountService spotifyAccountService, IHttpContextAccessor httpContextAccessor)
        {
            _spotifyAccountService = spotifyAccountService ?? throw new ArgumentNullException(nameof(spotifyAccountService));
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("LoginUri")]
        [HttpGet]
        public Uri GetSpotifyLoginUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            string url = $"{request.Scheme}://{request.Host.Value}";
            return _spotifyAccountService.GetSpotifyLoginUri(url);
        }

    }
}

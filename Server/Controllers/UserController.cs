using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Swan.Formatters;

namespace Blazify.Server.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SpotifyClientConfig _spotifyClientConfig;
        private readonly IConfiguration _configuration;

        public UserController(IHttpContextAccessor httpContextAccessor, SpotifyClientConfig spotifyClientConfig, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _spotifyClientConfig = spotifyClientConfig;
            _configuration = configuration;
        }

        [HttpGet("Login")]
        public ActionResult Login()
        {
            string clientId = _configuration.GetValue(typeof(string), "SPOTIFY_CLIENT_ID").ToString() ;

            var loginRequest = new LoginRequest(
                new Uri("https://localhost:44374/user/callback"),
                    clientId,
                        LoginRequest.ResponseType.Code
)
            {
                Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative }
            };

            var uri = loginRequest.ToUri();

            return Redirect(uri.AbsoluteUri);

        }

        [HttpGet("callback")]
        public async Task<ActionResult<object>> Callback([FromQuery] string code)
        {
            var response = new { text = "it works", code };

            await GetCallback(code);

            return response;
        }

        public async Task GetCallback(string code)
        {
            var uri = new Uri("https://localhost:44374/user/callback");
            string clientId = _configuration.GetValue(typeof(string), "SPOTIFY_CLIENT_ID").ToString();
            string clientSecret = _configuration.GetValue(typeof(string), "SPOTIFY_CLIENT_SECRET").ToString();   

            var response = await new OAuthClient().RequestToken(
            new AuthorizationCodeTokenRequest(clientId, clientSecret, code, uri)
            );
            var config  = SpotifyClientConfig
              .CreateDefault()
              .WithAuthenticator(new AuthorizationCodeAuthenticator(clientId, clientSecret, response));

            var spotify = new SpotifyClient(config);
          
        }




    }
}

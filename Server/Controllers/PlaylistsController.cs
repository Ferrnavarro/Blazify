using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazify.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace Blazify.Server.Controllers
{
    [Route("playlists")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly SpotifyClientBuilder _spotifyClientBuilder;

        public PlaylistsController(SpotifyClientBuilder spotifyClientBuilder)
        {
            _spotifyClientBuilder = spotifyClientBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<Paging<SimplePlaylist>>> GetPlaylists()
        {
            var spotify = await _spotifyClientBuilder.BuildClient();

            int offset = int.TryParse(Request.Query["Offset"], out offset) ? offset : 0;
            var playlistRequest = new PlaylistCurrentUsersRequest
            {
                Limit = 10,
                Offset = offset
            };
            var playlists = await spotify.Playlists.CurrentUsers(playlistRequest);

            return playlists;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Blazify.Server.Services.Interfaces;
using Blazify.Shared.Dtos.Input;
using Blazify.Shared.Models.Spotify;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace Blazify.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserTopController : ControllerBase
    {
        private readonly IUserTopService _userTopService;

        public UserTopController(IUserTopService userTopService)
        {
            _userTopService = userTopService;
        }

        [Route("tracks")]     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTopUserTracks([FromQuery] TopRequestDto requestInfo)
        {
            try
            {
                var tracks = await _userTopService.GetuserTopTracks(requestInfo.Token, requestInfo.TimeRange);
                return Ok(tracks);
            }
            catch (APIUnauthorizedException)
            {
                return Unauthorized();
            }
        }


        [Route("artists")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTopUserArtists([FromQuery] TopRequestDto requestInfo)
        {
            try
            {
                var artists = await _userTopService.GetuserTopArtists(requestInfo.Token, requestInfo.TimeRange);
                return Ok(artists);
            }
            catch (APIUnauthorizedException)
            {
                return Unauthorized();
            }
        }

    }
}

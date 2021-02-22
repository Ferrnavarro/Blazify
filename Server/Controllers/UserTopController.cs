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
            var tracks = await _userTopService.GetuserTopTracks(requestInfo.Token, requestInfo.TimeRange);

            if (tracks == null)
            {
                return NotFound();
            }

            return Ok(tracks);                   
        }


        [Route("prueba")]
        [HttpGet]
        public TopRequestDto GetInfo()
        {
           return  new TopRequestDto
            {
                Token = "12233",
                TimeRange = PersonalizationTopRequest.TimeRange.LongTerm
            };
        }


    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Blazify.Shared.Models.Spotify;
using SpotifyAPI.Web;

namespace Blazify.Server.Services.Interfaces
{
    public interface IUserTopService
    {
        Task<IEnumerable<Track>> GetuserTopTracks(string token, PersonalizationTopRequest.TimeRange timeRange);

        Task<IEnumerable<Artist>> GetuserTopArtists(string token, PersonalizationTopRequest.TimeRange timeRange);

    }
}

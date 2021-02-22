using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazify.Server.Extensions;
using Blazify.Server.Services.Interfaces;
using Blazify.Shared.Models.Spotify;
using SpotifyAPI.Web;

namespace Blazify.Server.Services
{
    public class UserTopService : IUserTopService
    {
        public Task<IEnumerable<Artist>> GetuserTopArtists(string token, PersonalizationTopRequest.TimeRange timeRange)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Track>> GetuserTopTracks(string token, PersonalizationTopRequest.TimeRange timeRange)
        {
            PersonalizationTopRequest request = new PersonalizationTopRequest
            {
                Limit = 50,
                TimeRangeParam = timeRange
            };

            var spotifyClient = GetSpotifyClient(token);

            var topTracks = await spotifyClient.Personalization.GetTopTracks(request);

            return topTracks.Items.GetTracks();
        }



        private SpotifyClient GetSpotifyClient(string token)
        {
            return new SpotifyClient(token);
        }   



    }
}

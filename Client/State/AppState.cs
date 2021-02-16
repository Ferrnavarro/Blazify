using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazify.Client.Dtos;
using Blazify.Client.Extensions;
using SpotifyAPI.Web;

namespace Blazify.Client.State
{
    public class AppState
    {
        public SpotifyClient SpotifyClient { get; set; }

        public List<TrackDto> TopTracks { get; private set; } = new List<TrackDto>();


        public async Task SetTopTracks(PersonalizationTopRequest.TimeRange timeRange)
        {
            PersonalizationTopRequest request = new PersonalizationTopRequest
            {
                Limit = 50,
                TimeRangeParam = timeRange
            };

            var topTracks = await SpotifyClient.Personalization.GetTopTracks(request);
         
            TopTracks = topTracks.Items.GetTracksDto();
        }

    }
}

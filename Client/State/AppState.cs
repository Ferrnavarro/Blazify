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
        private List<TrackDto> _longTermTracks;
        private List<TrackDto> _mediumTermTracks;
        private List<TrackDto> _shortTermTracks;

        private List<ArtistDto> _longTermArtists;
        private List<ArtistDto> _mediumTermArtists;
        private List<ArtistDto> _shortTermArtists;

        public SpotifyClient SpotifyClient { get; set; }

        public List<TrackDto> TopTracks { get; private set; } = new List<TrackDto>();
        public List<ArtistDto> TopArtists { get; set; } = new List<ArtistDto>();
        public List<TrackDto> RecentTracks { get; private set; } = new List<TrackDto>();



        public void SetTopTracks(PersonalizationTopRequest.TimeRange timeRange)
        {
            switch (timeRange)
            {
                case PersonalizationTopRequest.TimeRange.LongTerm:
                    TopTracks = _longTermTracks;
                    break;
                case PersonalizationTopRequest.TimeRange.MediumTerm:
                    TopTracks = _mediumTermTracks;
                    break;
                case PersonalizationTopRequest.TimeRange.ShortTerm:
                    TopTracks = _shortTermTracks;
                    break;
                default:
                    TopTracks = _longTermTracks;
                    break;
            }
        }

        public void SetTopArtists(PersonalizationTopRequest.TimeRange timeRange)
        {
            switch (timeRange)
            {
                case PersonalizationTopRequest.TimeRange.LongTerm:
                    TopArtists = _longTermArtists;
                    break;
                case PersonalizationTopRequest.TimeRange.MediumTerm:
                    TopArtists = _mediumTermArtists;
                    break;
                case PersonalizationTopRequest.TimeRange.ShortTerm:
                    TopArtists = _shortTermArtists;
                    break;
                default:
                    TopArtists = _longTermArtists;
                    break;
            }
        }


        public async Task InitiliazeTopTracks()
        {
            _longTermTracks = _longTermTracks != null && _longTermTracks.Any()? _longTermTracks:  await SetTracks(PersonalizationTopRequest.TimeRange.LongTerm);
            _mediumTermTracks = _mediumTermTracks != null && _mediumTermTracks.Any() ? _mediumTermTracks : await SetTracks(PersonalizationTopRequest.TimeRange.MediumTerm);
            _shortTermTracks = _shortTermTracks != null && _shortTermTracks.Any() ? _shortTermTracks : await SetTracks(PersonalizationTopRequest.TimeRange.ShortTerm);
        }

        public async Task InitiliazeTopArtistss()
        {
            _longTermArtists = _longTermArtists != null && _longTermArtists.Any() ? _longTermArtists : await SetArtists(PersonalizationTopRequest.TimeRange.LongTerm);
            _mediumTermArtists = _mediumTermArtists != null && _mediumTermArtists.Any() ? _mediumTermArtists : await SetArtists(PersonalizationTopRequest.TimeRange.MediumTerm);
            _shortTermArtists = _shortTermArtists != null && _shortTermArtists.Any() ? _shortTermArtists : await SetArtists(PersonalizationTopRequest.TimeRange.ShortTerm);
        }

        private async Task InitializeRecentTracks()
        {
            var recentTracksFromSpotify = await SpotifyClient.Player.GetRecentlyPlayed();

            RecentTracks = recentTracksFromSpotify.Items.GetTracksDto();
        }

        private async Task<List<TrackDto>> SetTracks(PersonalizationTopRequest.TimeRange timeRange)
        {
            PersonalizationTopRequest request = new PersonalizationTopRequest
            {
                Limit = 50,
                TimeRangeParam = timeRange
            };

            var topTracks = await SpotifyClient.Personalization.GetTopTracks(request);

            return topTracks.Items.GetTracksDto();

        }

        private async Task<List<ArtistDto>> SetArtists(PersonalizationTopRequest.TimeRange timeRange)
        {
            PersonalizationTopRequest request = new PersonalizationTopRequest
            {
                Limit = 50,
                TimeRangeParam = timeRange
            };

            var topTracks = await SpotifyClient.Personalization.GetTopArtists(request);

            return topTracks.Items.GetArtistsDto();

        }
       

        private LoginRequest GetSpotifyLoginRequest(Uri baseUri)
        {
            var clientId = "";

            return new LoginRequest(baseUri, clientId, LoginRequest.ResponseType.Token)
            {
                Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserTopRead },
            };
        }

        public string GetSpotifyLoginPageUrl(Uri baseUri)
        {
            var loginRequest = GetSpotifyLoginRequest(baseUri);
            return loginRequest.ToUri().ToString();
        }

    }
}

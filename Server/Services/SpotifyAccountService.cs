using System;
using Blazify.Server.Services.Interfaces;
using Blazify.Server.Settings.Interfaces;
using SpotifyAPI.Web;

namespace Blazify.Server.Services
{
    public class SpotifyAccountService : ISpotifyAccountService
    {
        private readonly ISpotifySettings _spotyfySettings;

        public SpotifyAccountService(ISpotifySettings spotyfySettings)
        {
            _spotyfySettings = spotyfySettings;
        }

        public Uri GetSpotifyLoginUri(string url)
        {
            var baseUri = new Uri(url);
            var clientId = _spotyfySettings.ClientId;

            var loginRequest = new LoginRequest(baseUri, clientId, LoginRequest.ResponseType.Token)
            {
                Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserTopRead },
            };

            return loginRequest.ToUri();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazify.Server.Settings.Interfaces;

namespace Blazify.Server.Settings
{
    public class SpotifySettings: ISpotifySettings
    {
        public string ClientId { get; set; }

    }
}

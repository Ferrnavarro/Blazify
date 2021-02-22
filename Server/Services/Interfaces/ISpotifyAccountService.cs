using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazify.Server.Services.Interfaces
{
    public interface ISpotifyAccountService
    {
        Uri GetSpotifyLoginUri(string baseUri);
    }
}

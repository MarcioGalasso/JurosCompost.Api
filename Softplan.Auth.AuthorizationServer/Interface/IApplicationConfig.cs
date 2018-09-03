using IdentityServer4.Models;
using System.Collections.Generic;


namespace Softplan.Auth.AuthorizationServer.Interface
{
    public interface IApplicationConfig
    {
        IEnumerable<ApiResource> GetApiResources();
        IEnumerable<Client> GetClients();
        
    }
}

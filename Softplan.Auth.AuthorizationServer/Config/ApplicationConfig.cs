
using System.Collections.Generic;
using Softplan.Auth.AuthorizationServer.Interface;
using IdentityServer4.Models;


namespace Softplan.Auth.AuthorizationServer.Config
{
    public class ApplicationConfig : IApplicationConfig
    {

        // scopes define the API resources in your system
        public  IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiScopeClientsComposite.ApiScopeClientsComposite().GetApis();
        }

        // client want to access resources (aka scopes)
        public  IEnumerable<Client> GetClients()
        {
            return new ApiScopeClientsComposite.ApiScopeClientsComposite().GetClients();
        }

    }
}

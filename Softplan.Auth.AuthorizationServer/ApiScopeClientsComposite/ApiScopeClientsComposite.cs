using Softplan.Auth.AuthorizationServer.ApiScopeClientsComposite.Juros;
using Softplan.Auth.AuthorizationServer.Interface;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.Auth.AuthorizationServer.ApiScopeClientsComposite
{
    public class ApiScopeClientsComposite
    {

        List<IApplicationConfigComposite> Applications = new List<IApplicationConfigComposite>
                                                                       {
                                                                           new JurosScopeClientsComposite()
                                                                       };


        public List<ApiResource> GetApis() {
            var apis = new List<ApiResource>();

            Applications.ForEach(c =>
            {
                apis = apis.Concat(c.GetApiResources()).ToList();
            });
            return apis;
        }

        public List<Client> GetClients()
        {
            var clients = new List<Client>();

            Applications.ForEach(c =>
            {
                clients = clients.Concat(c.GetClients()).ToList(); ;
            });
            return clients;
        }

    }
}

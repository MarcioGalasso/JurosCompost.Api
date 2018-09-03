using Softplan.Auth.AuthorizationServer.Interface;
using Softplan.Auth.AuthorizationServer.Model;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.Auth.AuthorizationServer.ApiScopeClientsComposite.Juros
{
    public class JurosScopeClientsComposite : IApplicationConfigComposite
    {
        public string Scope { get; set; } = "scope.juros";

        public List<ClientModel> ClientsId { get; set; } =
            new List<ClientModel> {
                                new ClientModel
                                {
                                    ClientId = "Softplan.Juros.Api",
                                    Secret = "secretSoftplan"
                                },
                                
        };

        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
                      {
                        new ApiResource(Scope, "API Juros"),
                      };
        }

        public IEnumerable<Client> GetClients()
        {
            var listClients = new List<Client>();
            ClientsId.ForEach(c =>
            {
                listClients.Add(
                    new Client
                    {
                        ClientId = c.ClientId,
                        AllowedGrantTypes = GrantTypes.ClientCredentials,

                        ClientSecrets =
                        {
                          new Secret(c.Secret.Sha256())
                        },
                        AllowedScopes = { Scope }
                    }
                    );

            });

            return listClients;
        }
    }
}

using Softplan.Auth.AuthorizationServer.Model;
using System.Collections.Generic;

namespace Softplan.Auth.AuthorizationServer.Interface
{
    public interface IApplicationConfigComposite : IApplicationConfig
    {
    
         string Scope { get; set; }
        List<ClientModel> ClientsId { get; set; }
    }
}

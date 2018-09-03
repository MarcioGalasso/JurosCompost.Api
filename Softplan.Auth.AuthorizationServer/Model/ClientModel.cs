using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.Auth.AuthorizationServer.Model
{
    public class ClientModel
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Contracts
{
    public interface IIpAdress
    {
        Task<string> GetIpConnect(HttpRequest request);
    }
}

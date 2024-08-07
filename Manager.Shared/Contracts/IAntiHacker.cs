using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Contracts
{
    public interface IAntiHacker
    {
        Task<bool> AntiHacker(string jwt);
    }
}

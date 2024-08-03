using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Models
{
    public class UserResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}

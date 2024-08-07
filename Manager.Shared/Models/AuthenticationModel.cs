using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Models
{
    [Table("Authentications")]
    public class AuthenticationModel
    {
        [Key]
        public int Id { get; set; }

        [Column("Jwt")]
        public string Jwt
        {
            get { return _jwt; }
            set { _jwt = value; }
        }
        private string _jwt = string.Empty;

    }
}

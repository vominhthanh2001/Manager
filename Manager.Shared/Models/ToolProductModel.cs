using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Models
{
    public class ToolProductModel
    {
        [Key]
        public int Id { get; set; }

        [Column("Name")]
        [Required(AllowEmptyStrings = true)]
        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string? _name;

        public override string? ToString()
        {
            return _name;
        }
    }
}

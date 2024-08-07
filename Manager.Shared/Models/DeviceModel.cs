using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Models
{
    [Table("Devices")]
    public class DeviceModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("MaxDevice")]
        public int MaxDevice
        {
            get { return _maxDevice; }
            set { _maxDevice = value; }
        }
        private int _maxDevice = 1;

        [Column("Online")]
        public int TotalOnline
        {
            get { return _totalOnline; }
            set { _totalOnline = value; }
        }
        private int _totalOnline;

        [Column("State")]
        public bool IsOnline { get; set; }

        public void ActionOnline()
        {
            IsOnline = true;
            TotalOnline++;
        }

        public bool ActionOffline()
        {
            TotalOnline -= 1;
            if (TotalOnline <= 0)
            {
                IsOnline = false;
                return true;
            }

            return false;
        }
    }
}

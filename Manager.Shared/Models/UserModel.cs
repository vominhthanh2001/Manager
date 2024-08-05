using Manager.Shared.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Models
{
    public class UserModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Column("Username")]
        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _userName = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [Column("Password")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _password = string.Empty;

        public string PasswordHash
        {
            get
            {
                return HashUtil.ComputeSHA512(_password);
            }
        }

        [Required(AllowEmptyStrings = false)]
        [Column("License")]
        public string License
        {
            get
            {
                var obj = new
                {
                    Username = _userName,
                    Password = _password,
                };

                string json = JsonConvert.SerializeObject(obj);
                return HashUtil.ComputeSHA512(json);
            }
        }

        [Required(AllowEmptyStrings = false)]
        [Column("TimeActive")]
        public DateTime TimeActive
        {
            get { return _timeActive; }
            set { _timeActive = value; }
        }
        private DateTime _timeActive = DateTimeUtil.GetCurrentTimeInVietnam();

        [Required(AllowEmptyStrings = false)]
        [Column("TimeExpired")]
        public DateTime TimeExpired
        {
            get { return _timeExpired; }
            set { _timeExpired = value; }
        }
        private DateTime _timeExpired = DateTimeUtil.GetCurrentTimeInVietnam();

        [Required(AllowEmptyStrings = false)]
        [Column("TimeRemaining")]
        public TimeSpan TimeRemaining
        {
            get { return _timeRemaining; }
        }
        private TimeSpan _timeRemaining => DateTimeUtil.GetTimeRemaining(_timeExpired);

        public string TitleTimeRemaining
        {
            get
            {
                string titleTimeRemaining = "Hết hạn sử dụng";

                TimeSpan remaining = _timeRemaining;
                if (remaining.Days > 0)
                    titleTimeRemaining += $"{remaining.Days} Ngày ";

                if (remaining.Days != 0 && remaining.Minutes != 0 && remaining.Seconds != 0)
                    titleTimeRemaining += $"{remaining.Minutes} Phút {remaining.Seconds} giây";

                return titleTimeRemaining;
            }
        }

        [Column("Role")]
        public RoleModel Role
        {
            get { return _role; }
            set { _role = value; }
        }
        private RoleModel _role = new RoleModel();

        [Column("Product")]
        public ProductModel Product
        {
            get { return _product; }
            set { _product = value; }
        }
        private ProductModel _product = new ProductModel();


        /// <summary>
        /// So sánh 2 mã hash
        /// </summary>
        /// <param name="passwordHashs"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool ComparePassword(string passwordHashs)
        {
            if (string.IsNullOrWhiteSpace(passwordHashs))
                throw new ArgumentException($"'{nameof(passwordHashs)}' cannot be null or whitespace.", nameof(passwordHashs));

            if (string.IsNullOrWhiteSpace(_password))
                throw new ArgumentException($"'{nameof(_password)}' cannot be null or whitespace.", nameof(_password));

            return HashUtil.CompareHashes(passwordHashs, _password);
        }

        public bool CompareDateTime()
        {
            return DateTimeUtil.CompareWithCurrentTimeInVietnam(_timeExpired);
        }

        public bool Vaild()
        {
            if (!string.IsNullOrWhiteSpace(License) && !string.IsNullOrWhiteSpace(_role.Role))
                return true;

            return false;
        }

        public string PasswordToDot()
        {
            string maskedPassword = new string('●', _password.Length);
            return maskedPassword;
        }
    }
}

using Manager.Shared.Utils;
using Newtonsoft.Json;
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

        [Required(AllowEmptyStrings = true)]
        [Column("Username")]
        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _userName;

        [Required(AllowEmptyStrings = true)]
        [Column("Password")]
        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value));
                
                _password = HashUtil.ComputeSHA512(value);
            }
        }
        private string _password;

        [Required(AllowEmptyStrings = true)]
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

        [Required(AllowEmptyStrings = true)]
        [Column("TimeActive")]
        public DateTime TimeActive
        {
            get { return _timeActive; }
            set { _timeActive = value; }
        }
        private DateTime _timeActive = DateTimeUtil.GetCurrentTimeInVietnam();

        [Required(AllowEmptyStrings = true)]
        [Column("TimeExpired")]
        public DateTime TimeExpired
        {
            get { return _timeExpired; }
            set { _timeExpired = value; }
        }
        private DateTime _timeExpired = DateTimeUtil.GetCurrentTimeInVietnam();

        [Required(AllowEmptyStrings = true)]
        [Column("TimeRemaining")]
        public TimeSpan TimeRemaining
        {
            get { return _timeRemaining; }
        }
        private TimeSpan _timeRemaining => DateTimeUtil.GetTimeRemaining(_timeExpired);

        private string _role = "User";
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

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
    }
}

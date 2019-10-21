using System;
using System.ComponentModel;

namespace BDDMsTestFeatureTests.Data
{
    public class UserDto
    {
        private DateTime _date;
        private string _firstname { get; set; }
        private string _lastname { get; set; }
        private string _uname { get; set; }

        public UserDto(bool blank = false)
        {
            if (!blank)
                SetDefaults();
        }
        public UserDto(UserDto dto)
        {
            if (dto == null)
                SetDefaults();
        }
        private void SetDefaults()
        {

            _date = DateTime.Now;
            _firstname = $"LNAUTOFIRST{_date.ToString("MMddyyhh")}";
            _lastname = $"LNAUTOLAST{_date.ToString("MMddyy")}";
            _uname = $"LNAUTOUNAME{_date.ToString("dd")}{_date.ToString("mmss")}";

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
            {
                DefaultValueAttribute myAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];
                if (myAttribute != null)
                    property.SetValue(this, myAttribute.Value);
                else if (property.Name == "FirstName")
                    property.SetValue(this, _firstname);
                else if (property.Name == "LastName")
                    property.SetValue(this, _lastname);
                else if (property.Name == "Uname")
                    property.SetValue(this, _uname);

            }

        }
        /// <summary>
        /// User's FirstName
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        ///  User's FirstName
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// street in user's address
        /// </summary>
        [DefaultValue("1214, Office Park Dr")]
        public string Street { get; set; }
        /// <summary>
        /// City in user's address
        /// </summary>
        [DefaultValue("Oxford")]
        public string City { get; set; }
        /// <summary>
        /// State in user's address
        /// </summary>
        [DefaultValue("MS")]
        public string State { get; set; }
        /// <summary>
        /// Zipcode in user's address
        /// </summary>
        [DefaultValue("38665")]
        public string Zip { get; set; }
        /// <summary>
        /// User's Phone Number
        /// </summary>
        [DefaultValue("7305621922")]
        public string Phone { get; set; }
        /// <summary>
        /// Users SSN
        [DefaultValue("888999777")]
        public string SSN { get; set; }
        /// <summary>
        /// User's Login ID
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// Parabank Common Password
        /// </summary>
        [DefaultValue("ParaBank123")]
        public string PassWord { get; set; }

    }
}

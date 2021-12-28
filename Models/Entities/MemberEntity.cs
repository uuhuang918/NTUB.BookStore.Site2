using NTUB.BookStore.Site.Models.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Entities
{
	public class MemberEntity
	{
        public const string Salt= "!@#$$DGTEGYT";
        public int Id { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }
		public string  EncryptedPassword
		{
			get
			{
                string salt = "!@#$$DGTEGYT";
                string result = HashUtility.ToSHA256(this.Password, salt);
                return result;
			}
        }

		public string Name { get; set; }

        public string Mobile { get; set; }

        public bool IsConfirmed { get; set; }

        public string ConfirmCode { get; set; }

        public string Email { get; set; }
    }
}
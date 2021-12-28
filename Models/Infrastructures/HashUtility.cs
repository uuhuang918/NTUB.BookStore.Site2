using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NTUB.BookStore.Site.Models.Infrastructures
{
	public static class HashUtility
	{
		public static string ToSHA256(string plainText,string salt)
		{
			using(SHA256 mySHA256=SHA256.Create())
			{
				var passwordBytes = Encoding.UTF8.GetBytes(salt + plainText);
				var hash = mySHA256.ComputeHash(passwordBytes);
				StringBuilder sb=new StringBuilder();
				foreach (var b in hash)
				{
					sb.Append(b.ToString("X2"));
				}
		         return sb.ToString();
			}
		}
	}
}
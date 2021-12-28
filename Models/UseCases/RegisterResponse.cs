using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.UseCases
{
	public class RegisterResponse
	{
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public RegisterEntity Data { get; set; }
	}
}
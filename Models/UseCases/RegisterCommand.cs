using NTUB.BookStore.Site.Models.Core;
using NTUB.BookStore.Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.UseCases
{
	public class RegisterCommand
	{
		public RegisterResponse Execute(RegisterVM model)
		{
			var service = new MemberService();
			RegisterRequest request = model.ToRequest();
			RegisterResponse response = service.CreateNewMember(request);
			if(response.IsSuccess==true)
			{
				//send email
			}
			return response;
		}
	}
}
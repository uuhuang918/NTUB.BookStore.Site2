using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Core
{
	public class CustomerService
	{
		private readonly ICustomerRepository _repository;
		public CustomerService(ICustomerRepository repository)
		{
			_repository = repository;
		}

		public  CustomerEntiity Load(string customerAccount)
			=>_repository.IsExists(customerAccount)
		    ?Load(customerAccount)
			:throw new Exception("找不到有權限購物的客戶資訊");

		public int GetCustomerId(string customerAccount)
			=> Load(customerAccount).Id;
	}
}
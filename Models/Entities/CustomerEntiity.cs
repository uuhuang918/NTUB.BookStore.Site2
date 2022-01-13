using NTUB.BookStore.Site.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Entities
{
	public class CustomerEntiity
	{
		public int Id { get; set; }
		public string CustomerAccount { get; set; }
	}


	public static class CustomerExts
	{
		public static CustomerEntiity ToCustomerEntiity(this Member source)
			=>new CustomerEntiity { Id=source.Id, CustomerAccount=source.Account};
	}


}
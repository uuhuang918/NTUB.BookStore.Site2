using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.EFModels;
using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Infrastructures.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{

		private readonly AppDbContext _db;

		public CustomerRepository(AppDbContext db)
		{
			_db= db;
		}


		public int GetCustomerId(string customeAccount)
		 =>_db.Members.Single(x=>x.IsConfirmed==true&&x.Account==customeAccount).Id;


		/// <summary>
		/// 有權限再傳購物的會員才傳回true
		/// </summary>
		/// <param name="customeAccount"></param>
		/// <returns></returns>
		public bool IsExists(string customeAccount)
		{
			var member=_db.Members.SingleOrDefault(x=>x.IsConfirmed==true&&x.Account==customeAccount);
			return member!=null;
		}

		public CustomerEntiity Load(string customeAccount)
		{
			return _db.Members.Single(x => x.IsConfirmed == true && x.Account == customeAccount).ToCustomerEntiity();
		}
	}
}
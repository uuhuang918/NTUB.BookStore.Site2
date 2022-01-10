using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTUB.BookStore.Site.Models.Core.Interfaces
{
	public interface ICustomerRepository
	{
		bool IsExists(string customeAccount);


		int GetCustomerId(string customeAccount);

		CustomerEntiity Load(string customeAccount);
	}
}

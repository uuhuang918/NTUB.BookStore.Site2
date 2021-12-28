using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTUB.BookStore.Site.Models.Core.Interfaces
{
	public interface IMemberRepository
	{
		bool IsExist(string account);
		void Create(MemberEntity entity);
		MemberEntity Load (int memberId);
		MemberEntity Load (string account);
		void ActiveRegister(int memberId);
	}

	
}

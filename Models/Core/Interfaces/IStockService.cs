using NTUB.BookStore.Site.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTUB.BookStore.Site.Models.Core.Interfaces
{
	/// <summary>
	/// 供前台網站使用，在結帳時，用來異動各商品庫存量
	/// </summary>
	public interface IStockService
	{
		/// <summary>
		/// 新增訂單時，用來扣除庫存量
		/// </summary>
		/// <param name="info"></param>
		void Deduct(DeductStockInfo[] info);

		/// <summary>
		/// 客戶退訂尚未出貨的商品時，自動加回庫存量
		/// </summary>
		/// <param name="info"></param>
		void Revise(ReviseStockInfo[] info);	
	}
}

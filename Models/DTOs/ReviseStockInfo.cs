using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.DTOs
{
	public class ReviseStockInfo
	{
		/// <summary>
		/// 訂單取消時，要增加庫存量的資訊
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// 傳入正數
		/// </summary>
		public int Qty { get; set; }
	}
}
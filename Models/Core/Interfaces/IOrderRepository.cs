using NTUB.BookStore.Site.Models.DTOs;
using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTUB.BookStore.Site.Models.Core.Interfaces
{
	public interface IOrderRepository
	{
		/// <summary>
		/// 結帳時，建立一筆新紀錄和明細
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		int Create(CreateOrderRequest request);

		/// <summary>
		/// 前台由客戶提出退訂申請
		/// </summary>
		/// <param name="orderId"></param>
		void RefundByCustomer(int orderId);

		/// <summary>
		/// 傳回訂單
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>

		OrderEntity Load(int orderId);

		IEnumerable<OrderEntity> Search(string customerAccount,DateTime? startTime,DateTime? endTime);
	}
}

using NTUB.BookStore.Site.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTUB.BookStore.Site.Models.Core.Interfaces
{
	internal interface IOrderService
	{
		/// <summary>
		/// 新訂單建立後，觸發本事件
		/// StockService可以訂閱並扣庫存(可以由mediator操作)
		/// </summary>
		event Action<IOrderService, int> OrderCreate;

		/// <summary>
		/// 可以由客戶提出退訂申請後，觸發本事件
		/// StockService可以訂閱，如果退定的訂單還沒出貨，直接加回庫存(可以由mediator操作)
		/// </summary>

		event Action<IOrderService, int> RefundRequestByCustomer;

		/// <summary>
		/// 結帳，建立一個新訂單及明細
		/// </summary>
		/// <param name="request"></param>
		void PlaceOrder(CreateOrderRequest request);


		/// <summary>
		/// 只有客戶本人可以提出退訂申請，訂單必須符合退訂條件才行
		/// 訂單必須還沒完成出貨或出貨七天內，才允許由使用者退貨本功能只供會員提出退貨申請，會註記在Order.RequestRefund
		/// 如果訂單尚未出貨，直接同意退訂，若已經出貨，須由後台審核之後才算正式成立並更新庫存
		/// </summary>
		/// <param name="customerAccount"></param>
		/// <param name="orderId"></param>
		void Refund(string customerAccount, int orderId);


	}
}

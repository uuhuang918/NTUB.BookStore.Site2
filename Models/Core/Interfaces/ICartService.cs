using NTUB.BookStore.Site.Models.DTOs;
using NTUB.BookStore.Site.Models.Entities;
using NTUB.BookStore.Site.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTUB.BookStore.Site.Models.Core.Interfaces
{
	public interface ICartService
	{
		/// <summary>
		/// 客戶發出結帳請求，第二個參數是customerAccount
		/// </summary>
		event Action<ICartService, string> RequestCheckout;

		/// <summary>
		///每一個會員在同一時間裡，只會有一筆購物車,若結帳時,將它清;若沒購物車紀錄，自動建立一筆 
		/// </summary>
		/// <param name="customerAccount"></param>
		/// <returns></returns>
		CartEntity Current(string customerAccount);

		/// <summary>
		/// 若購物車已有相同商品，則數量加一:若無則新增一個CartItem
		/// </summary>
		/// <param name="customerAccount"></param>
		/// <param name="productId"></param>
		/// <param name="qty"></param>

		void AddItem(string customerAccount, int productId,int qty=1);

		void UpdateItem(string customerAccount, int productId, int newQty);
		void RemoveItem(string customerAccount, int productId);
		void EmptyCart(string customerAccount);

		/// <summary>
		/// 客戶發出結帳的請求，會觸發RequestCheckout
		/// </summary>
		/// <param name="customerAccount"></param>
		/// <param name="shippingInfo"></param>
		void Checkout(string customerAccount,ShippingInfo shippingInfo);
		CreateOrderRequest ToCreateOrderRequest(CartEntity cart);
		DeductStockInfo[] GetDeductStockInfo(CartEntity cart);

	}
}

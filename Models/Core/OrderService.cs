using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.DTOs;
using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Core
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _repository;
		public event Action<IOrderService, int> OrderCreated;
		public event Action<IOrderService, int> RefundRequestByCustomer;

		public OrderService(IOrderRepository repository)
		{
			_repository = repository;
		}



		public void PlaceOrder(CreateOrderRequest request)
		{
			int orderId = _repository.Create(request);
			OnOrderCreated(orderId);
		}


		protected virtual void OnOrderCreated(int orderId)
		{
			if(OrderCreated != null)
			{
				OrderCreated(this, orderId);
			}
		}

		public void Refund(string customerAccount, int orderId)
		{
			OrderEntity orderEntity=_repository.Load(orderId);
			if (orderEntity == null) throw new Exception("找不到訂單紀錄");
			if (string.Compare(customerAccount, orderEntity.CustomerAccount, StringComparison.CurrentCultureIgnoreCase) != 0)
			{
				throw new Exception("您無法對他人訂單進行退訂");
			}

			if (orderEntity.AllowRefund == false) throw new Exception("此訂單目前無法進行退訂");

			_repository.RefundByCustomer(orderId);
			//raise event
			OnRefundRequestByCustomer(orderId);
		}


		protected virtual void OnRefundRequestByCustomer(int orderId)
		{
			if(RefundRequestByCustomer != null)
			{
				RefundRequestByCustomer(this, orderId);
			}
		}
	}
}
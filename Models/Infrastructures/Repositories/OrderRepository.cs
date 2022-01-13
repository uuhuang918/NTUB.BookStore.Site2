using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.DTOs;
using NTUB.BookStore.Site.Models.EFModels;
using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Infrastructures.Repositories
{
	public class OrderRepository : IOrderRepository
	{
	
			private readonly AppDbContext _db;

			public OrderRepository(AppDbContext db)
			{
				_db = db;
			}



			public int Create(CreateOrderRequest request)
		{
			var order = new Order
			{
				MemberId = request.CustomerId,
				Total = request.Items.Sum(x => x.SubTotal),
				CreatedTime = DateTime.Now,
				Status = 1,
				Receiver = request.ShippingInfo.Receiver,
				Address = request.ShippingInfo.Address,
				CellPhone = request.ShippingInfo.CellPhone,
				OrderItems = request.Items.Select(x => x.ToEF()).ToList(),
			};
			_db.Orders.Add(order);
			_db.SaveChanges();

			return order.Id;
		}

		public OrderEntity Load(int orderId)
		{
			throw new NotImplementedException();
		}

		public void RefundByCustomer(int orderId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<OrderEntity> Search(string customerAccount, DateTime? startTime, DateTime? endTime)
		{
			throw new NotImplementedException();
		}
	}
}
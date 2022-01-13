using NTUB.BookStore.Site.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Entities
{
	public class OrderItemEntity
	{
		public OrderItemEntity(int id,OrderProductEntity product,int qty)
		{
			Id = id;
			Product = product;
			Qty = qty;
		}
		public int Id { get; set; }
		private OrderProductEntity _Product;                                         
		public OrderProductEntity Product
		{ get => _Product;
			set => _Product = value != null ? value : throw new Exception("Product不能是null");
				}

		private int _Qty;
		public int Qty
		{
			get => _Qty;
			set => _Qty = value > 0 ? value : throw new Exception("Qty必須大於0");

		}

		public int SubTotal=>Product.Price*Qty;

	}


	public static partial class OrderItemExts
	{
		public static OrderItemEntity ToEntity(this OrderItem source)
		{
			return new OrderItemEntity(source.Id,source.Product.ToOrderProductEntity(),source.Qty);
		}
	}
}
using NTUB.BookStore.Site.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.DTOs
{
	public class CreateOrderRequest
	{
		public int CustomerId { get; set; }
		public List<CreateOrderItem> Items { get; set; }
		public ShippingInfo ShippingInfo { get; set; }
		public int Total => Items.Sum(x=>x.SubTotal);
	}
}
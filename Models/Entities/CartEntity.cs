using NTUB.BookStore.Site.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Entities
{
	public class CartEntity
	{
		public CartEntity(int id,string customerAccount)
		{
			this.Id = id;
			this.CustomerAccount = customerAccount;
			Items = new List<CartItemEntity>();
		}

		public CartEntity(int id, string customerAccount,List<CartItemEntity>items)
		{
			this.Id=id;
			this.CustomerAccount = customerAccount;
			Items=items;
		}

		public int Id { get; set; }
		private string _CustomerAccount;
		public string CustomerAccount
		{
			get => _CustomerAccount;
			set => _CustomerAccount = string.IsNullOrEmpty(value) == false ? value : throw new Exception("CustomerAccount不能是空值");
		}
		private List<CartItemEntity> Items;
		public int Total => Items == null || Items.Count == 0 ? 0 : Items.Sum(x => x.SubTotal);
		public  ShippingInfo ShippingInfo { get; set; }

		public void AddItem(CartProductEntity product,int qty)
		{
			if(product == null)throw new ArgumentNullException(nameof(product));
			if(qty <= 0)throw new ArgumentOutOfRangeException(nameof(qty));

			var item=Items.SingleOrDefault(x => x.Product.Id ==product.Id);
			if(item == null)
			{
				var cartItem=new CartItemEntity(product,qty);
				this.Items.Add(cartItem);
			}
			else
			{
				item.Qty = qty;
			}
		}



	}
}
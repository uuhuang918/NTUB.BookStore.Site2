using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.EFModels;
using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NTUB.BookStore.Site.Models.Infrastructures.Repositories
{
	public class CartRepository : ICartRepository

	{
		private readonly AppDbContext _db;

		public CartRepository(AppDbContext db)
		{
			_db = db;	
		}

		/// <summary>
		/// 判斷之前先用IsExists判斷，沒有才叫用
		/// </summary>
		/// <param name="customerAccount"></param>
		/// <returns></returns>
		public CartEntity CreateNewCart(string customerAccount)
		{
			var cart =new Cart { MemberAccount = customerAccount };
			_db.Carts.Add(cart);
			_db.SaveChanges();
			return  Load (customerAccount);
		}

		public void EmptyCart(string customerAccount)
		{
			var cart=_db.Carts.SingleOrDefault(x=>x.MemberAccount == customerAccount);
			if (cart == null) return;
			_db.Carts.Remove(cart);
			_db.SaveChanges ();
		}

		public bool IsExists(string customerAccount)
		=>_db.Carts.SingleOrDefault(x=>x.MemberAccount == customerAccount)!=null;



		public CartEntity Load(string customerAccount)
		=> _db.Carts
			.AsNoTracking()
			.Include(cart=>cart.CartItems.Select(cartItem=>cartItem.Product))
			.Single(cart => cart.MemberAccount == customerAccount).ToEntity();

		public void Save(CartEntity cart)
		{
			Cart cartEF = cart.ToEF();

			var efInDb = _db.Carts.Include(x => x.CartItems).Single(x => x.Id == cart.Id);

			var efItemsInDb=efInDb.CartItems.ToList();

			var deletedProducts=efItemsInDb.Select(x=>x.ProductId)
				.Except(cartEF.CartItems.Select(x=>x.ProductId)).ToList();

			foreach(var productId in deletedProducts)
			{
				var delItem = efInDb.CartItems.Single(x => x.ProductId == productId);
				_db.Entry(delItem).State = EntityState.Deleted;
			}

			foreach (var item in cartEF.CartItems)
			{
				if(item.Id == 0)
				{
					efInDb.CartItems.Add(item);
				}
			else
				{
					efInDb.CartItems.Single(x=>x.Id==item.Id).Qty = item.Qty;
				}
			}
			_db.SaveChanges();
		}
	}
}
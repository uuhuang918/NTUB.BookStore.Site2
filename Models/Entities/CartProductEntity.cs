﻿using NTUB.BookStore.Site.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Entities
{
	/// <summary>
	/// 供購物車使用的商品類別，僅包含必要資訊
	/// </summary>
	public class CartProductEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Price { get; set; }
	}


	public static partial class ProductExts
	{
		public static CartProductEntity ToCartProduct(this Product source)
			=>new CartProductEntity { Id=source.Id, Name=source.Name, Price=source.Price };
	}
}
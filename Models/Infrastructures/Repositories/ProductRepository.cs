using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.EFModels;
using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Infrastructures.Repositories
{
	public class ProductRepository : IProductRepository
	{

		private readonly AppDbContext _db;
		public ProductRepository(AppDbContext db)
		{
			_db = db;
		}

		public ProductEntity Load(int? productId, bool? status)
		{
			IEnumerable<Product> query = _db.Products.AsNoTracking().Where(x=>x.Id==productId);
			if(status.HasValue)query=query.Where(x=>x.Status==status);
			Product product = query.SingleOrDefault();
			return product==null?null:product.ToEntity();
		}

		public IEnumerable<ProductEntity> Search(int? categoryId, string productName, bool? status)
		{
			IEnumerable<Product> query = _db.Products.AsNoTracking();

			if(categoryId.HasValue)query=query.Where(x=>x.CategoryId == categoryId);
			if(!string.IsNullOrEmpty(productName))query=query.Where(x=>x.Name.Contains(productName));
			if(status.HasValue)query.Where(x=>x.Status == status);
			query=query.OrderBy(x=>x.Name);
			return query.Select(x => x.ToEntity());
		}
	}
}
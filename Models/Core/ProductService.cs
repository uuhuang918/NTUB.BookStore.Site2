using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Core.Interfaces
{
	public class ProductService
	{
		private readonly IProductRepository _repository;
		public  ProductService(IProductRepository repository)
		{
			_repository = repository;
		}

		public IEnumerable<ProductEntity> SearchActiveProducts(int?categoryId,string productName)
			=>_repository.Search(categoryId,productName,true);

		public ProductEntity LoadActiveProduct(int producId)
			=>_repository.Load(producId,true);
	}
}
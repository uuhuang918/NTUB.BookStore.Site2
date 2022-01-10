using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTUB.BookStore.Site.Models.Core.Interfaces
{
	public interface IProductRepository
	{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="categoryId"></param>
	/// <param name="productName"></param>
	/// <param name="status"></param>
	/// <returns></returns>
		IEnumerable<ProductEntity> Search(int? categoryId, string productName, bool? status);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		ProductEntity Load(int? productId, bool? status);
	}
}

using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.ViewModels
{
	public class ProductVM
	{
        public int Id { get; set; }

        [Display(Name="名稱")]
        public string Name { get; set; }

        [Display(Name = "分類名稱")]
        public string CategoryName { get; set; }

        [Display(Name = "商品描述")]
        public string Description { get; set; }

        [Display(Name = "售價")]
        public int Price { get; set; }


        [Display(Name = "商品照片")]
        public string ProductImage { get; set; }

        [Display(Name = "庫存量")]
        public int Stock { get; set; }
    }

    public static partial class ProductEntityExts
	{
        public static ProductVM ToVM(this ProductEntity source)
		{
            return new ProductVM
            {
                Id = source.Id,
                Name = source.Name,
                CategoryName = source.Category.Name,
                Description = source.Description,
                Price = source.Price,
                Stock = source.Stock,
                ProductImage = source.ProductImage,
            };
		}
	}
}
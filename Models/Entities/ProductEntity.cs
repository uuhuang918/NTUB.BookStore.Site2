using NTUB.BookStore.Site.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Entities
{
	public class ProductEntity
	{
        public int Id { get; set; }

        public CategoryEntity Category { get; set; }

      
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool Status { get; set; }

        public string ProductImage { get; set; }

        public int Stock { get; set; }
    }

    public static partial class ProductExts
	{
        public static ProductEntity ToEntity(this Product source)
            => new ProductEntity
            {
                Id = source.Id,
                Category = source.Category.ToEntity(),
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                Status = source.Status,
                ProductImage = source.ProductImage,
                Stock = source.Stock,

            };
	}
}
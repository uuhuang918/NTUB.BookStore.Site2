using NTUB.BookStore.Site.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Entities
{
	public class CategoryEntity
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }
    }
    public static partial class CategoryExts
    {
        public static CategoryEntity ToEntity(this Category source)
              => new CategoryEntity
              {
                  Id = source.Id,
                  Name = source.Name,
                  DisplayOrder = source.DisplayOrder,

              };
    }
}
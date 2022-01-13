using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.EFModels;
using NTUB.BookStore.Site.Models.Infrastructures.Repositories;
using NTUB.BookStore.Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTUB.BookStore.Site.Controllers
{
    public class ProductsController : Controller
    {
        private ProductService productService;


        public ProductsController()
		{
            var db = new AppDbContext();
            IProductRepository repo = new ProductRepository(db);
            this.productService = new ProductService(repo);
		}

        // GET: Products
        public ActionResult Index()
        {
            var data = productService.SearchActiveProducts(null, null).Select(x => x.ToVM());
            return View(data);
        }
    }
}
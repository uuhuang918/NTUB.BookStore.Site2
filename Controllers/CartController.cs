using NTUB.BookStore.Site.Models.Core;
using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.EFModels;
using NTUB.BookStore.Site.Models.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTUB.BookStore.Site.Controllers
{
    public class CartController : Controller
    {

        private ICartService cartService;
        public CartController()
		{
            AppDbContext db = new AppDbContext();
            ICartRepository cartRepo = new CartRepository(db);
            IProductRepository productRepo=new ProductRepository(db);
            ICustomerRepository customerRepo=new CustomerRepository(db);
            this.cartService = new CartService(cartRepo, productRepo, customerRepo);
		}

        private string CustomerAccount=>User.Identity.Name;

        [Authorize]
        // GET: Cart/AddItem
        public ActionResult AddItem(int productId)
        {
            cartService.AddItem(CustomerAccount, productId, 1);
            return new EmptyResult();
        }

        public ActionResult Info()
		{

		}
    }
}
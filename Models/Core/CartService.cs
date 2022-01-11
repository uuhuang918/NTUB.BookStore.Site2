using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.DTOs;
using NTUB.BookStore.Site.Models.Entities;
using NTUB.BookStore.Site.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Core
{
	public class CartService : ICartService
	{
		public event Action<ICartService, string> RequestCheckout;

		private readonly ICartRepository _repository;
		private readonly IProductRepository _productRepo;
		private readonly ICustomerRepository _customerRepo;

		public CartService(ICartRepository repository,IProductRepository productRepo,ICustomerRepository customerRepo)
		{
			_repository = repository;
			_productRepo = productRepo;
			_customerRepo = customerRepo;
		}


		private ShippingInfo shippingInfo;

		public void Checkout(string customerAccount, ShippingInfo shippingInfo)
		{
			if (string.IsNullOrEmpty(customerAccount)) throw new ArgumentNullException(nameof(customerAccount));
			if (shippingInfo == null) throw new Exception("ShippingInfo not allow null");
			this.shippingInfo = shippingInfo; //先暫存，ToCreateOrderRequest會用到
			OnRequestCheckout(customerAccount); //觸發RequestCheckout事件
		}

		protected virtual void OnRequestCheckout(string customerAccount)
		{
			if(RequestCheckout!=null)
			{
				RequestCheckout(this, customerAccount);
			}
		}
		public void AddItem(string customerAccount, int productId, int qty = 1)
		{
			var cart=Current(customerAccount);

			var product=_productRepo.Load(productId, true);
			var cartProd = new CartProductEntity { Id=productId,Name=product.Name,Price=product.Price};
			cart.AddItem(cartProd, qty);
			_repository.Save(cart);
		}

		public CartEntity Current(string customerAccount)
		{
			if(_repository.IsExists(customerAccount))
			{
				return _repository.Load(customerAccount);
			}
			else
			{
				return _repository.CreateNewCart(customerAccount);
			}
		}

		public void EmptyCart(string customerAccount)
		{
			_repository.EmptyCart(customerAccount);
		}

		public DeductStockInfo[] GetDeductStockInfo(CartEntity cart)
		{
			return cart.GetItems().Select(x => new DeductStockInfo { ProductId = x.Product.Id, Qty = x.Qty }).ToArray();
		}

		public void RemoveItem(string customerAccount, int productId)
		{
			var cart = Current(customerAccount);
			cart.RemoveItem(productId);
			_repository.Save(cart);
		}

		public CreateOrderRequest ToCreateOrderRequest(CartEntity cart)
		{
			List<CreateOrderItem> items = cart.GetItems().Select(x => new CreateOrderItem
			{
				ProductId=x.Product.Id,
				ProductName=x.Product.Name,
				Price=x.Product.Price,
				Qty=x.Qty
			}).ToList();
			return new CreateOrderRequest
			{
				CustomerId = _customerRepo.GetCustomerId(cart.CustomerAccount),
				Items = items,
				ShippingInfo = this.shippingInfo
			};
		}

		public void UpdateItem(string customerAccount, int productId, int newQty)
		{
			var cart = Current(customerAccount);
			cart.UpdateQty(productId, newQty);
			_repository.Save(cart);
		}
	}
}
﻿using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Core
{
	public class StockService : IStockService
	{

		private readonly IStockRepository _repository;
		public StockService(IStockRepository repository)
		{
			_repository = repository;
		}

		public void Deduct(DeductStockInfo[] info)
		{
			var tuple=info.Select(x=>(x.ProductId,x.Qty*-1)).ToArray();
			_repository.Update(tuple);
		}

		public void Revise(ReviseStockInfo[] info)
		{
			var tuple = info.Select(x => (x.ProductId, x.Qty * -1)).ToArray();
			_repository.Update(tuple);
		}
	}
}
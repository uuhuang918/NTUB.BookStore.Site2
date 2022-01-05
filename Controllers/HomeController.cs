using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NTUB.BookStore.Site.Controllers
{
	public class HomeController : Controller
	{
		private string Path
		{
			get
			{
				string folder = ConfigurationManager.AppSettings
					["productImageFolder"].ToString();
				return Server.MapPath(folder);
			}
		}

		public ActionResult Index()
		{
			ViewBag.Path = Path;	
			return View();
		}

		[Authorize]
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Members/Login");
		}
	}
}
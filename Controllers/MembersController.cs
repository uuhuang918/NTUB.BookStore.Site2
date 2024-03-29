﻿using NTUB.BookStore.Site.Models.Core;
using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.DTOs;
using NTUB.BookStore.Site.Models.Entities;
using NTUB.BookStore.Site.Models.Infrastructures.Repositories;
using NTUB.BookStore.Site.Models.UseCases;
using NTUB.BookStore.Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NTUB.BookStore.Site.Controllers
{
    public class MembersController : Controller
    {
        IMemberRepository repo=new MemberRepository();
        MemberService service;
        public MembersController()
		{
            service = new MemberService(repo);
		}

        public ActionResult Index()
		{
            return View();
		}
        // GET: Members/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                RegisterCommand command = new RegisterCommand();
                RegisterResponse response = command.Execute(model);
                if (response.IsSuccess)
                {
                    return View("RegisterConfirm");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.ErrorMessage);
                    return View(model);
                }
            }

            else
            {
                return View(model);
            }
        }
        public ActionResult ActiveRegister(int memberId,string confirmCode)
		{
            IMemberRepository repo = new MemberRepository();
            var service = new MemberService(repo);
            service.ActiveRegister(memberId, confirmCode);
            return View();
		}
        public ActionResult Login()
		{
            return View();
		}
        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            LoginResponse response = service.Login(model.Account, model.Password);
            if(response.IsSuccess)
			{
                var rememberMe = false;
                string returnUrl = ProcessLogin(model.Account,rememberMe,out HttpCookie cookie);
                Response.Cookies.Add(cookie);
                return Redirect(returnUrl);
			}
            ModelState.AddModelError(string.Empty,response.ErrorMessage);
            return this.View(model);
        }

		

		private string ProcessLogin(string account,bool rememberMe,out HttpCookie cookie)
		{
            var member = repo.Load(account);
            string roles=String.Empty;
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1,
                account,DateTime.Now, DateTime.Now.AddDays(2), rememberMe,roles, "/");
            string value = FormsAuthentication.Encrypt(ticket);
            cookie = new HttpCookie(FormsAuthentication.FormsCookieName,value);
            string url= FormsAuthentication.GetRedirectUrl(account,true);
            return url;

        }
       
        public ActionResult Logout()
		{
            Session.Abandon();
            FormsAuthentication.SignOut();
            return Redirect("/Members/Login");
		}

        [Authorize]
        public ActionResult EditProfile()
		{
            string currentUserAccount= User.Identity.Name;
            MemberEntity entity=repo.Load(currentUserAccount);
            EditProfileVM model = entity.ToEditProfileVM();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(EditProfileVM model)
        {
            string currentUserAccount = User.Identity.Name;

            if (ModelState.IsValid==false)
			{
                return View(model);
			}
            UpdateProfileRequest request = model.ToRequest(currentUserAccount);
            try
			{
                service.UpdateProfile(request);
			}
            catch (Exception ex)
			{
                ModelState.AddModelError(string.Empty,ex.Message);
			}
            if(ModelState.IsValid)
			{
                if (string .Compare(User.Identity.Name,model.Account)==0)
				{
                    return RedirectToAction("Index");
				}
                else
				{
                    return RedirectToAction("Logout");
				}
			}
            else
			{
                return View(model);
            }
        }

        [Authorize]
        public ActionResult EditPassword()
		{
            return View();
		}

        [HttpPost]
        [Authorize]
        public ActionResult EditPassword(EditPasswordVM model)
        {
            string currentUserAccount = User.Identity.Name;

            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            ChangePasswordRequest request = model.ToRequest(currentUserAccount);
            try
            {
                service.ChangePassword(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            if (ModelState.IsValid)
            {
                    return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordVM model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            try
			{
                string urlTemplate = Request.Url.Scheme + "://" + Request.Url.Authority+Url.Content("~/") + 
                    "Members/ResetPassword?memberid={0}&confirmCode={1}";

                service.RequestResetPassword(model.Account, model.Email,urlTemplate);

                return View("ConfirmForgetPassword");
			}
			catch(Exception ex)
			{
                ModelState.AddModelError(string.Empty,ex.Message);
			}
            return View(model);
        }

        public ActionResult ResetPassword(int memberId,string confirmCode)
		{
            return View();
		}

        [HttpPost]
        public ActionResult ResetPassword(int memberId, string confirmCode,ResetPasswordVM model)
		{
            if (ModelState.IsValid==false)
			{
                return View(model);
			}
            try
			{
                service.ResetPassword(memberId, confirmCode, model.Password);
			}
            catch (Exception ex)
			{
                ModelState.AddModelError(string.Empty, ex.Message);
			}
            if(ModelState.IsValid==false)
			{
                return View(model);
			}
			else
			{
                return View("ResetPasswordConfirm");
			}
		}

    }
}
using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.DTOs;
using NTUB.BookStore.Site.Models.EFModels;
using NTUB.BookStore.Site.Models.Entities;
using NTUB.BookStore.Site.Models.Infrastructures;
using NTUB.BookStore.Site.Models.Infrastructures.Repositories;
using NTUB.BookStore.Site.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Core
{
	public class MemberService
	{
		private readonly IMemberRepository repository;
		public MemberService()
		{
			this.repository = new MemberRepository();
		}
		public MemberService(IMemberRepository repo)
		{
			this.repository = repo;
		}
		public RegisterResponse CreateNewMember(RegisterRequest request)
		{
			if (repository.IsExist(request.Account))
			{
				return new RegisterResponse { IsSuccess = false, ErrorMessage = "帳號已存在" };

			}
			string confirmCode = Guid.NewGuid().ToString("N");   // "N" →不要有dash 

			MemberEntity entity = new MemberEntity
			{
				Account = request.Account,
				Password = request.Password,
				Email = request.Email,
				Name = request.Name,
				Mobile = request.Mobile,
				IsConfirmed = false,
				ConfirmCode = confirmCode,
			};
			repository.Create(entity);
			return new RegisterResponse
			{
				IsSuccess = true,
				Data = new RegisterEntity
				{
					Name = request.Name,
					Email = request.Email,
					ConfirmCode=confirmCode,
				}
			};
		}

		public void ActiveRegister(int memberId,string confirmCode)
		{
			MemberEntity entity = repository.Load(memberId);
			if (entity == null) return;
			if (string.Compare(entity.ConfirmCode, confirmCode) != 0) return;
			repository.ActiveRegister(memberId);
		}
		public LoginResponse Login(string account,string password)
		{
			MemberEntity member = repository.Load(account);
			if (member == null)
			{
				return LoginResponse.Fail("帳密有誤");
			}
			if(member.IsConfirmed==false)
			{
				return LoginResponse.Fail("會員資格尚未確認");
			}
			string encryptedPassword=HashUtility.ToSHA256(password,MemberEntity.Salt);
			return (String.CompareOrdinal(member.Password,encryptedPassword) == 0)
			?LoginResponse.Success()
			:LoginResponse.Fail("帳密有誤");
			
		}

		public void UpdateProfile(UpdateProfileRequest request)
		{
			MemberEntity entity = repository.Load(request.CurrentUserAccount);
			if (entity == null) throw new Exception("找不到要修改的會員紀錄");

			bool isExists = repository.IsExist(request.Account, entity.Id);
			if (isExists) throw new Exception("帳號已被使用過，無法變更");

			entity.Name=request.Name;
			entity.Mobile=request.Mobile;
			entity.Email=request.Email;
			entity.Account=request.Account;

			repository.Update(entity);
		}

		public void ChangePassword(ChangePasswordRequest request)
		{
			MemberEntity entity = repository.Load(request.CurrentUserAccount);
			if (entity == null) throw new Exception("找不到要修改的會員紀錄");

			string encryptedPassword=HashUtility.ToSHA256(request.OriginalPassword,MemberEntity.Salt);
			bool isSamePassword = string.Compare(encryptedPassword, entity.Password) == 0;
			if (isSamePassword) throw new Exception("原始密碼不符，無法變更");

			//更新紀錄
			entity.Password = HashUtility.ToSHA256(request.NewPassword, MemberEntity.Salt);
			repository.UpdatePassword(entity);
		}
	}

}
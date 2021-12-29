using NTUB.BookStore.Site.Models.Core.Interfaces;
using NTUB.BookStore.Site.Models.EFModels;
using NTUB.BookStore.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.Infrastructures.Repositories
{
	public class MemberRepository:IMemberRepository
	{
		private AppDbContext db = new AppDbContext();

		public void ActiveRegister(int memberId)
		{
			var member = db.Members.Find(memberId);
			member.IsConfirmed = true;
			member.ConfirmCode = null;
			db.SaveChanges();
		}

		public void Update(MemberEntityWithoutPassword entity)
		{
			var member = db.Members.Find(entity.Id);

			member.Account = entity.Account;
			member.Mobile = entity.Mobile;
			member.IsConfirmed = entity.IsConfirmed;
			member.ConfirmCode = entity.ConfirmCode;
			member.Email = entity.Email;

			db.SaveChanges();
		}

		public void UpdatePassword(int memberId, string encryptedPassword)
		{
			var member = db.Members.Find(memberId);

			member.Password=encryptedPassword;

			db.SaveChanges();
		}
		public void Create(MemberEntity entity)
		{
			Member member = new Member
			{
				Account = entity.Account,
				Password = entity.EncryptedPassword,    //entity.Password,
				Email = entity.Email,
				Name = entity.Name,
				Mobile = entity.Mobile,
				IsConfirmed = entity.IsConfirmed,
				ConfirmCode = entity.ConfirmCode,
			};

			db.Members.Add(member);
			db.SaveChanges();
		}

		public bool IsExist(string account)
		{
		   var entity = db.Members.SingleOrDefault(x => x.Account == account);
			if (entity == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool IsExist(string account,int excludeId)
		{
			var entity = db.Members.SingleOrDefault(x => x.Id != excludeId&& x.Account==account);
			return entity != null;
		}

		public MemberEntity Load(int memberId)
		{
			return db.Members.SingleOrDefault(x=>x.Id==memberId).ToEntity();
			//Member entity = db.Members.SingleOrDefault(x=>x.Id==memberId);
			//MemberEntity result = new MemberEntity
			//{
				//Id = entity.Id,
				//Account = entity.Account,
				//Password = entity.Password,
				//Email = entity.Email,
				//Name = entity.Name,
				//Mobile = entity.Mobile,
				//IsConfirmed = entity.IsConfirmed,
				//ConfirmCode = entity.ConfirmCode,
			//};
			//return result;
		}

		public MemberEntity Load(string account)
		  =>db.Members.SingleOrDefault(x => x.Account == account).ToEntity();

	}
	public static class MemberEntityExtensions
	{
		public static MemberEntity ToEntity(this Member entity)
		{
			return new MemberEntity
			{
				Id = entity.Id,
				Account = entity.Account,
				Password = entity.Password,
				Email = entity.Email,
				Name = entity.Name,
				Mobile = entity.Mobile,
				IsConfirmed = entity.IsConfirmed,
				ConfirmCode = entity.ConfirmCode,
			};
		}
	}
}
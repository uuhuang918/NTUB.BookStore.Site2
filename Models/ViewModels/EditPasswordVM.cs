using NTUB.BookStore.Site.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.ViewModels
{
	public class EditPasswordVM
	{
		[Display(Name = "原本密碼")]
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string OriginalPassword { get; set; }

		[Display(Name = "修改密碼")]
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "再次輸入密碼")]
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}

	public static class EditPasswordViewModelExtensions
	{
		public static ChangePasswordRequest ToRequest(this EditPasswordVM source, string userAccount)
		{
			return new ChangePasswordRequest
			{
				CurrentUserAccount = userAccount,
				OriginalPassword = source.OriginalPassword,
				NewPassword=source.Password,
			};

		}
	}
}
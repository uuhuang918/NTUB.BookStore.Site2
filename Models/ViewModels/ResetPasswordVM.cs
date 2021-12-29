using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.ViewModels
{
	public class ResetPasswordVM
	{
		[Display(Name = "新密碼")]
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "確認密碼")]
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
} 
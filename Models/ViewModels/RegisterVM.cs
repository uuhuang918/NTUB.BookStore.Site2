using NTUB.BookStore.Site.Models.UseCases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.ViewModels
{
	public class RegisterVM
	{


       [Display(Name ="帳號")]
        [Required]
        [StringLength(30)]
        public string Account { get; set; }

        [Display(Name = "密碼")]
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Display(Name = "再次輸入密碼")]
        [Required]
        [StringLength(50)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


        [Display(Name = "姓名")]
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "手機")]
        [Required]
        [StringLength(10)]
        public string Mobile { get; set; }


        [Display(Name = "電子信箱")]
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }
    }

    public static class RegisterVMExts
	{
        public static RegisterRequest ToRequest(this RegisterVM sourse)
		{
            return new RegisterRequest
            {
                Account = sourse.Account,
                Password = sourse.Password,
                Email = sourse.Email,
                Name = sourse.Name,
                Mobile = sourse.Mobile,
            };
		}
	}
}

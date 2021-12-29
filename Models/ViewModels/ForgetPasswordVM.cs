using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTUB.BookStore.Site.Models.ViewModels
{
    public partial class ForgetPasswordVM
    {
       
        [Required]
        [StringLength(30)]
        public string Account { get; set; }


        [Required]
        [StringLength(256)]
        public string Email { get; set; }
    }
}
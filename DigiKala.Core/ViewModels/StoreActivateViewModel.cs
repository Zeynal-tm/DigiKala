using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DigiKala.Core.ViewModels
{
    public class StoreActivateViewModel
    {
        [Display(Name = "کد موبایل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [Phone(ErrorMessage = "فقط عدد می توانید وارد کنید")]
        [MaxLength(6, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(6, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]

        public string MobileCode { get; set; }

        [Display(Name = "کد ایمیل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [Phone(ErrorMessage = "فقط عدد می توانید وارد کنید")]
        [MaxLength(6, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(6, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]

        public string MailCode { get; set; }
    }
}

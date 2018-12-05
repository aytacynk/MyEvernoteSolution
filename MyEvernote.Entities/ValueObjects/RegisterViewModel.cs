using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Kullancı Adı"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = ("{0} max. {1} karakter olmalı."))]
        public string Username { get; set; }

        [DisplayName("Email Adresi"),
            DataType(DataType.EmailAddress),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(70, ErrorMessage = ("{0} max. {1} karakter olmalı.")),
            EmailAddress(ErrorMessage = ("{0} alanı için geçerli bir e-posta adresi giriniz."))]
        public string EMail { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = ("{0} alanı boş geçilemez.")),
            StringLength(25, ErrorMessage = ("{0} max {1} karakter olmalı.")),
            DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"),
            Required(ErrorMessage = ("{0} alanı boş geçilemez.")),
            StringLength(25, ErrorMessage = ("{0} max {1} karakter olmalı.")),
            DataType(DataType.Password), 
            Compare("Password", ErrorMessage = ("{0} ile {1} uyuşmamaktadır."))]
        public string RePassword { get; set; }
    }
}
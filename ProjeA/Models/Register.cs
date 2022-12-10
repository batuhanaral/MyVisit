using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjeA.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string Name { get; set; }


        [Required]
        [DisplayName("Kullanıcı Soyadı")]
        public string SurName { get; set; }


        [Required]
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }


        [Required]
        [DisplayName("Eposta")]
        [EmailAddress(ErrorMessage = "Eposta adresiniz hatalıdır.")]
        public string Email { get; set; }


        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Şifrenizde en az 8 karakterden oluşmalı ve en az bir büyük harf,bir küçük harf ve bir rakam bulunmalıdır")]
        [Required]
        [DisplayName("Parola")]
        public string Password { get; set; }


        [Required]
        [DisplayName("Parola Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor")]
        public string RePassword { get; set; }
    }
}
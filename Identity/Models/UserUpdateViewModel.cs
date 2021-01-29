using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserUpdateViewModel
    {
        public string PictureUrl { get; set; }
        public IFormFile Picture { get; set; }
        [Display(Name = "Telefon Numarası:")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir email adresi giriniz.")]
        public  string Email { get; set; }
        [Display(Name = "Isim:")]
        [Required(ErrorMessage = "Isim alanı gereklidir.")]
        public string Name { get; set; }
        [Display(Name = "Soyisim:")]
        [Required(ErrorMessage = "Soyisim alanı gereklidir.")]
        public string SurName { get; set; }

    }
}

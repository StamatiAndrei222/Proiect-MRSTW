using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.User
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "Numele de utilizator este obligatoriu.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Numele de utilizator trebuie să aibă între 3 și 50 de caractere.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Adresa de email este obligatorie.")]
        [EmailAddress(ErrorMessage = "Adresa de email nu este validă.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Parola trebuie să aibă cel puțin 6 caractere.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Parola și confirmarea parolei nu se potrivesc.")]
        public string ConfirmPassword { get; set; }
    }
}
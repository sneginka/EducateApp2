﻿using System.ComponentModel.DataAnnotations;

namespace EducateApp2.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите E-mail")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]

        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите ввод пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердить пароль")]
        [DataType(DataType.Password)]

        public string PasswordConfirm { get; set; }
    }
}

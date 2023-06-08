using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.ViewModels.Identity;

public class RegisterUserViewModel
{
    public RegisterUserViewModel()
    {
        UserName = null!;
        Password = null!;
        PasswordConfirm = null!;
    }

    [Required(ErrorMessage="Имя пользователя не указано")]
    [MaxLength(255)]
    [Display(Name = "Имя пользователя")]
    [Remote("IsNameFree", "Account")]
    public string UserName { get; set;}

    [Required(ErrorMessage = "Пароль является обязательным")]
    [MaxLength(255)]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set;}

    [Required(ErrorMessage = "Не введено подтверждение пароля")]
    [MaxLength(255)]
    [Display(Name = "Подтверждение пароля")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Пароль и подтверждение не совпадают")]
    public string PasswordConfirm { get; set;}
}

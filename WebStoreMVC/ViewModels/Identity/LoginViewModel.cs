using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.ViewModels.Identity;

public class LoginViewModel
{
    public LoginViewModel()
    {
        UserName = null!;
        Password = null!;
    }

    [Required(ErrorMessage="Имя пользователя не указано")]
    [MaxLength(255)]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set;}

    [Required(ErrorMessage = "Пароль является обязательным")]
    [MaxLength(255)]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set;}

    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string? ReturnUrl { get; set; }
}
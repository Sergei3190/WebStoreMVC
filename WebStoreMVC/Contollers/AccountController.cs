using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain.Identity;
using WebStoreMVC.ViewModels.Identity;

namespace WebStoreMVC.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager; 
        _logger = logger;   
    }

    [AllowAnonymous]
    public IActionResult Register() => View(new RegisterUserViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var user = new User()
        {
            UserName = viewModel.UserName,  
        };

        var creation_result = await _userManager.CreateAsync(user, viewModel.Password);

        if (creation_result.Succeeded) 
        {
            _logger.LogInformation("Пользователь {0} зарегистрирован", user);

            await _userManager.AddToRoleAsync(user, Role.Users);
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");   
        }

        foreach (var error in creation_result.Errors)
            ModelState.AddModelError("", error.Description);

        var errorInfo = string.Join(", ", creation_result.Errors.Select(e => e.Description));
        _logger.LogWarning("Ошибка при регистрации пользователя {0}: {1}", user, errorInfo);

        return View(viewModel);  
    }

    [AllowAnonymous]
    public IActionResult Login(string? returnUrl) => View(new LoginViewModel() { ReturnUrl = returnUrl });

    [HttpPost]
    [ValidateAntiForgeryToken]
	[AllowAnonymous]
	public async Task<IActionResult> Login(LoginViewModel viewModel) 
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var loginResault = await _signInManager.PasswordSignInAsync(
            viewModel.UserName,
            viewModel.Password, 
            viewModel.RememberMe,
            lockoutOnFailure : true);

        if (loginResault.Succeeded) 
        {
            _logger.LogInformation("Пользователь {0} успешно вошёл в систему", viewModel.UserName);

            return LocalRedirect(viewModel.ReturnUrl ?? "/");
        }

        ModelState.AddModelError("", "Неверное имя пользователя или пароль");

        _logger.LogWarning("Ошибка входа пользователя {0}: неверное имя или пароль", viewModel.UserName);

        return View(viewModel);
    }

    public async Task<IActionResult> Logout()
    {
        var userName = User.Identity!.Name;

        await _signInManager.SignOutAsync();

        _logger.LogInformation("Пользователь {0} вышел из системы", userName);

        return RedirectToAction("Index", "Home");
    }

	[AllowAnonymous]
	public IActionResult AccessDenied(string? returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;  
        return View();
    }
}
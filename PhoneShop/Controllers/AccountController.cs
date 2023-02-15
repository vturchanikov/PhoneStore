using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInmanager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInmanager = signInManager;
    }

    public async Task<IActionResult> Register(string returnUrl = null)
    {
        RegisterViewModel registerViewModel = new RegisterViewModel();
        registerViewModel.ReturnUrl = returnUrl;

        return View(registerViewModel);
    }
}

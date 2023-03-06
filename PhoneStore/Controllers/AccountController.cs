using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneStore.Interfaces;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ISendGridEmail _sendGridEmail;

    public AccountController(UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ISendGridEmail sendGridEmail)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _sendGridEmail = sendGridEmail;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult ResetPassword(string code = null)
    {
        return code == null ? View("Error") : View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
            if(user == null)
            {
                ModelState.AddModelError("Email", "UserNotFound");
                return View();
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code,
                resetPasswordViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }
        }
        return View(resetPasswordViewModel);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        LoginViewModel loginViewModel = new LoginViewModel();
        loginViewModel.ReturnUrl = returnUrl ?? Url.Content("~/");

        return View(loginViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password,
                loginViewModel.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Store");
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return View(loginViewModel);
            }
        }

        return View(loginViewModel);
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);

            await _sendGridEmail.SendEmailAsync(model.Email, "Reset Email Confirmation", "Please reset email by going to this link" +
                "<a href=\"" + callbackurl + "\"> link</a>");

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Register(string? returnUrl = null)
    {
        if(!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Customer"));
        }
        /*
        List<SelectListItem> listItems = new List<SelectListItem>();
        listItems.Add(new SelectListItem()
        {
            Value = "Admin",
            Text = "Admin"
        });
        listItems.Add(new SelectListItem()
        {
            Value = "Customer",
            Text = "Customer"
        });*/

        RegisterViewModel registerViewModel = new RegisterViewModel();
        //registerViewModel.RoleList = listItems;
        registerViewModel.ReturnUrl = returnUrl;

        return View(registerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string? returnUrl)
    {
        registerViewModel.ReturnUrl = returnUrl;
        returnUrl = returnUrl ?? Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = new AppUser { Email = registerViewModel.Email, UserName = registerViewModel.UserName };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                if(registerViewModel.RoleSelected != null && registerViewModel.RoleSelected.Length > 0 &&
                    registerViewModel.RoleSelected == "Admin")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("Password", "User could not be created. Password not unique enough");
        }

        return View(registerViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Store");
    }

}

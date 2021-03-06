﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Financee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Financee.App.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<FinanceeUser> _signInManager;
        private readonly UserManager<FinanceeUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<FinanceeUser> userManager,
            SignInManager<FinanceeUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Потребителско име")]
            [StringLength(100, ErrorMessage = "Потребителското име трябва да е с дължина поне {2} и най-много {1} символа",
                MinimumLength = 3)]
            public string Nickname { get; set; }

            [Required]
            [EmailAddress(ErrorMessage = "Това не е валиден имейл адрес")]
            [Display(Name = "Имейл адрес")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0}та трябва да е с дължина поне {2} и най-много {1} символа",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повтори паролата")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new FinanceeUser { UserName = Input.Nickname, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (this._signInManager.UserManager.Users.Count() == 1)
                {
                    var roleResult = this._signInManager.UserManager.AddToRoleAsync(user, "Admin").Result;
                }
                else
                {
                    var roleResult = this._signInManager.UserManager.AddToRoleAsync(user, "User").Result;
                }
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
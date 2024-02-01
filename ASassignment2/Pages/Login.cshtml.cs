using ASassignment2.Model;
using ASassignment2.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASassignment2.Pages
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public Login LModel { get; set; }

		private readonly IHttpContextAccessor context;
		private UserManager<ApplicationUser> userManager {  get; }

		private readonly SignInManager<ApplicationUser> signInManager;
		public LoginModel (SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			context = httpContextAccessor;
		}
		public void OnGet()
		{

		}
		//Login form
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
				var protector = dataProtectionProvider.CreateProtector("MySecretKey");
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
				LModel.RememberMe, true);
				if (identityResult.Succeeded)
				{
					//context.HttpContext.Session.Clear();
					//context.HttpContext.Session.CommitAsync();

					var user = await userManager.FindByEmailAsync(LModel.Email);

                    

                    if (user != null)
					{
						context.HttpContext.Session.SetString("FullName", user.FullName);
                        context.HttpContext.Session.SetString("CreditCard", protector.Unprotect(user.CreditCard));
                        context.HttpContext.Session.SetString("Gender", user.Gender);
                        context.HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber);
                        context.HttpContext.Session.SetString("Address", user.Address);
                        context.HttpContext.Session.SetString("Email", user.Email);
                        context.HttpContext.Session.SetString("AboutMe", user.AboutMe);

						var authCookieValue = Guid.NewGuid().ToString(); //generate unique value
						context.HttpContext.Response.Cookies.Append("AppendCookie", authCookieValue);
						context.HttpContext.Session.SetString("AuthCookie", authCookieValue);
						user.AuthToken = authCookieValue;
						await userManager.UpdateAsync(user);
								
                    }

                    return RedirectToPage("Index");
				}
				ModelState.AddModelError("", "Account is locked out");
			}
			return Page();	
		}
	}
}

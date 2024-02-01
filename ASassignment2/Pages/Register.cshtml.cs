using ASassignment2.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace ASassignment2.Pages
{
	public class RegisterModel : PageModel
	{
		private UserManager<ApplicationUser> userManager { get; }
		private SignInManager<ApplicationUser> signInManager { get; }
		[BindProperty]
		public Register RModel { get; set; }

		public RegisterModel (UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public void OnGet()
		{



		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var dataProtectionprovider = DataProtectionProvider.Create("EncryptData");
				var protector = dataProtectionprovider.CreateProtector("MySecretKey");


				var user = new ApplicationUser()
				{
					UserName = HttpUtility.HtmlEncode(RModel.Email),
					Email = HttpUtility.HtmlEncode(RModel.Email),
					FullName = HttpUtility.HtmlEncode(RModel.FullName),
					CreditCard = protector.Protect(RModel.CreditCard),
					PhoneNumber = HttpUtility.HtmlEncode(RModel.PhoneNumber),
					Address = HttpUtility.HtmlEncode(RModel.Address),
					AboutMe = HttpUtility.HtmlEncode(RModel.AboutMe),
					Gender = HttpUtility.HtmlEncode(RModel.Gender),
					Photo = HttpUtility.HtmlEncode(RModel.Photo),
				};
				var result = await userManager.CreateAsync(user, RModel.Password);
				if (result.Succeeded)
				{
					
					return RedirectToPage("Login");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return Page();


			
		}
	}
}

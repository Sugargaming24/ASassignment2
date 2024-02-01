using ASassignment2.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_Core_Identity.Model;

namespace ASassignment2.Pages
{
	[Authorize]
	public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor context;

        private readonly AuthDbContext authContext;
        
        private UserManager<ApplicationUser> userManager { get;  }
        private SignInManager<ApplicationUser> signInManager { get; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AuthDbContext authContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            context = httpContextAccessor;
            this.authContext = authContext;
        }

        public async Task<IActionResult> OnGet()
        {
            var authCookieValue = context.HttpContext.Request.Cookies["AuthCookie"];
            var sessionAuthToken = context.HttpContext.Session.GetString("AuthCookie");
            var user = await userManager.GetUserAsync(User);

            if (sessionAuthToken != user.AuthToken)
            {
                user.AuthToken = null;
                await userManager.UpdateAsync(user);

                await signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
            var log = new AuditLog
            {
                UserID = user.Id,
                Activity = "Logged In",
                Time = DateTime.Now,
            };

            authContext.AuditLogTable.Add(log);
            authContext.SaveChanges();
            return Page();


        }
    }
}
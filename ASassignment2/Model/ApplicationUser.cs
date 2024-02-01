using Microsoft.AspNetCore.Identity;

namespace ASassignment2.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string CreditCard { get; set; }
        public string PhoneNumber {  get; set; }
        public string Address { get; set; }
        public string AboutMe { get; set; }
        public string Gender { get; set; }
        public string Photo {  get; set; }
		public string? AuthToken { get; internal set; }
	}
}

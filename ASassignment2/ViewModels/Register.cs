using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace ASassignment2
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Invalid Email Address!")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage ="Only text is allowed")]
        public string FullName {  get; set; }

        [Required(ErrorMessage = "Credit Card is required")]
        [CreditCard(ErrorMessage = "Invalid credit card number")]
        [DataType(DataType.CreditCard)]
        public string CreditCard { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone Number must be 8 digits")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Photo is required")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg", ErrorMessage = "Only JPG files are allowed")]
        public string Photo { get; set; }


        [Required]
        [DataType(DataType.Text)]
        public string AboutMe { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,20}$", 
            ErrorMessage = "Password must be at least 12 characters long, contain at least one lowercase letter, one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }
    }
}

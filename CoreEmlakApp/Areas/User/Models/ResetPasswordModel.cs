using System.ComponentModel.DataAnnotations;

namespace CoreEmlakApp.Areas.User.Models
{
    public class ResetPasswordModel
    {
        
        [Required(ErrorMessage = "Not Empty")]
        [EmailAddress(ErrorMessage = "Invalid Email Type")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Not Empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password Type")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Not Empty")]
        [Compare("Password", ErrorMessage = "Passwords No Match")]
        public string ConfirmPassword { get; set; }
    }
}

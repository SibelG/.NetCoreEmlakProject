using System.ComponentModel.DataAnnotations;

namespace CoreEmlakApp.Areas.User.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Not Empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password Type")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Not Empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password Type")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Not Empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password Type")]
        public string ConfirmPassword { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace CoreEmlakApp.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Not Empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Not Empty")]
        [DataType(DataType.Password,ErrorMessage ="Invalid Password Type")]
        public string Password { get; set; }    

    }
}

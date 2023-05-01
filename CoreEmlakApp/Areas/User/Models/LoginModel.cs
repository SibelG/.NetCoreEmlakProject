using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmlakApp.Areas.User.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Not Empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Not Empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password Type")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}

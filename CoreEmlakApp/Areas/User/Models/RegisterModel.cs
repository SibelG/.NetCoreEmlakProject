using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmlakApp.Areas.User.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Not Empty")]
        public string Email  { get; set; }

        [Required(ErrorMessage = "Not Empty")]
        [DataType(DataType.Password, ErrorMessage="Invalid Password Type ")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Invalid Password Type")]
        [Required(ErrorMessage = "Not Empty")]
        [Compare("Password", ErrorMessage = "Passwords No Match")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Not Empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Not Empty")]
        public string FullName { get; set; }

    }
}

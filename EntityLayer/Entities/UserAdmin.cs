using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class UserAdmin:IdentityUser
    {
        public UserAdmin()
        {
            Adverts = new List<Advert>();
            Types = new List<Type>();
        }
        public string FullName { get; set; }

        public virtual List<Advert> Adverts { get; set; }

        public virtual List<Type> Types { get; set; }


    }
}

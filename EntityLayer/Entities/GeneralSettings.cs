using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
  
    public class GeneralSettings
    {
        [Key]
        public int GenerationSettingsId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public  string Address { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

    }
}

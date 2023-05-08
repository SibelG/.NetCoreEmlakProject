using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Front
    {
        [Key]
        public int FrontId { get; set; }
        public string FrontName { get; set; }
        public bool Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Heading
    {
        [Key]
        public int HeadingId { get; set; }
        public string HeadingName { get; set; }
        public bool Status { get; set; }
    }
}

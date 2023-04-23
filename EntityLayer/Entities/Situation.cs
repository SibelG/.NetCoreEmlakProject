using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Situation
    {
        [Key]
        public int SituationId { get; set; }
        public string SituationName { get; set; }
        public bool Status { get; set; }
    }
}

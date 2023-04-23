using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Type
    {
        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public bool Status { get; set; }

        public int SituationId { get; set; }

        public virtual Situation Situation { get; set; }

        public virtual List<Advert> Advert { get; set; }
    }
}

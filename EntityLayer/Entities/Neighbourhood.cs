using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Neighbourhood
    {
        [Key]
        public int NeighbourhoodId { get; set; }
        public string NeighbourhoodName { get; set; }
        public bool Status { get; set; }

        public int DistrictId { get; set; }

        public virtual  District District { get; set; }
    }
}

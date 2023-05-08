using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public  class FuelType
    {
        [Key]
        public int FuelTypeId { get; set; }
        public string FuelTypeName { get; set; }
        public bool Status { get; set; }

    }
}

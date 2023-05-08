using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Category
    {
        public Category()
        {
            Types = new List<EntityLayer.Entities.Type>();
        }
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public bool Status { get; set; }
        public virtual List<EntityLayer.Entities.Type> Types { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Images
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public bool Status { get; set; }
        public int AdvertId { get; set; }
        public virtual Advert Advert { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class ProjectImage
    {
        [Key]
        public int ProjectImageId { get; set; }
        public string ImageName { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public int ProjectId { get; set; }
        public virtual Projects Project { get; set; }
    }
}

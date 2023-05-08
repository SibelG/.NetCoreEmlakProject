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
    public class Projects
    {

        public Projects()
        {

            Images = new List<ProjectImage>();
           
          
        }
        [Key]
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }

        public string ProjectCompany { get; set; }
        public string Description { get; set; }

        public string Address { get; set; }
   
        public string Area { get; set; }
      
        public int CategoryId { get; set; }
        public bool UseCase { get; set; }

        public decimal Price { get; set; }

        public string DeliveryDate { get; set; }

        public string BuildingDelivery { get; set; }
        public string FloorCount { get; set; }
        public string NumberOfRooms { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public string TypeCount { get; set; }

        public string Elevator { get; set; }
        public bool CarPark { get;  set; }

        public bool Status { get; set; }

        [NotMapped]
        public IEnumerable<IFormFile> Image { get; set; }

        public int NeighbourhoodId { get; set; }
        public virtual Neighbourhood Neighbourhood { get; set; }
        public int SituationId { get; set; }
        public virtual Situation Situation { get; set; }


        public int TypeId { get; set; }
        public virtual Type Type { get; set; }

        public int HeadingId { get; set; }

        public virtual Heading Heading { get; set; }


        public virtual List<ProjectImage> Images { get; set; }
    }
}


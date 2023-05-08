using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Advert
    {

        public Advert() {
            Images = new List<Images>();
        }
        [Key]
        public int AdvertId { get; set; }
        public string AdvertTitle { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Garage { get; set; }
        public bool Garden { get; set; }
        public bool Fireplace { get; set; }
        public bool Furniture { get; set; }
        public bool Pool { get; set; }
        public bool AirCoordinator { get; set; }
        public int NumberOfRooms { get; set; }
        public int BathRoomNumbers { get; set; }
        public int CategoryId { get; set; }
        public bool UseCase { get; set; }
        public bool AuthorizedOffice { get; set; }

        public bool VideoBrowsing { get; set; }
    
        public decimal Subscription { get; set; }
        public decimal Deposit { get; set; }

        public int LivingRoomCount { get; set; }

        public int BuildingAge { get; set; }
       

        public bool Credid { get; set; }

        public bool Teras { get; set; }
        public int Area { get; set; }

        public DateTime AdvertDate { get; set; }
        public int Floor { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        public string UserAdminId { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public IEnumerable<IFormFile> Image { get; set; }

        public int NeighbourhoodId { get; set; }
        public virtual Neighbourhood Neighbourhood { get; set; }
        public int SituationId { get; set; }
        public virtual Situation Situation { get; set; }

     
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }

        public int FuelTypeId { get; set; }
        public virtual FuelType FuelType {get;set;}

        public int FrontId { get; set; }
        public virtual Front Front { get; set; }
      
       
        public virtual UserAdmin UserAdmin { get; set; }
        public virtual List<Images> Images { get; set; }
    }
}

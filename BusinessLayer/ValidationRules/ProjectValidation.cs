using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProjectValidation : AbstractValidator<Projects>
    {
        public ProjectValidation() {
            RuleFor(x => x.ProjectTitle).NotEmpty().WithMessage("Address Title is not empty");
            RuleFor(x => x.ProjectTitle).MinimumLength(2).MaximumLength(500).WithMessage("Address Title is not empty");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is not empty");
            RuleFor(x => x.NumberOfRooms).NotEmpty().WithMessage("Room number is not empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is not empty");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Area is not empty");
            RuleFor(x => x.FloorCount).NotEmpty().WithMessage("Floor Count is not empty");
            RuleFor(x => x.CarPark).NotEmpty().WithMessage("CarPark is not empty");
            RuleFor(x => x.DeliveryDate).NotEmpty().WithMessage("DeliveryDate is not empty");
            RuleFor(x => x.BuildingDelivery).NotEmpty().WithMessage("Building Delivery is not empty");
            RuleFor(x => x.Elevator).NotEmpty().WithMessage("Elevator is not empty");
            RuleFor(x => x.NeighbourhoodId).NotEmpty().WithMessage("NeighbourId is not empty");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("DistricId is not empty");
            RuleFor(x => x.ProjectCompany).NotEmpty().WithMessage("Company is not empty");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("DistricId is not empty");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("CityId is not empty");
            RuleFor(x => x.TypeId).NotEmpty().WithMessage("TypeId is not empty");
            RuleFor(x => x.SituationId).NotEmpty().WithMessage("SituationId is not empty");
        }  
    }
}

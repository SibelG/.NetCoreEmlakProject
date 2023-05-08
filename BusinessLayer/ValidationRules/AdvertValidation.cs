using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.ValidadionRules
{
    public class AdvertValidation:AbstractValidator<Advert>
    {
        public AdvertValidation() { 
            RuleFor(x=>x.AdvertTitle).NotEmpty().WithMessage("Address Title is not empty");
            RuleFor(x => x.AdvertTitle).MinimumLength(2).MaximumLength(500).WithMessage("Address Title is not empty");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is not empty");
            RuleFor(x => x.BathRoomNumbers).NotEmpty().WithMessage("Bath count is not empty");
            RuleFor(x => x.NumberOfRooms).NotEmpty().WithMessage("Room number is not empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is not empty");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Area is not empty");
            RuleFor(x => x.Floor).NotEmpty().WithMessage("Floor is not empty");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is not empty");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is not empty");
            RuleFor(x => x.NeighbourhoodId).NotEmpty().WithMessage("NeighbourId is not empty");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("DistricId is not empty");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is not empty");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("CityId is not empty");
            RuleFor(x => x.TypeId).NotEmpty().WithMessage("TypeId is not empty");
            RuleFor(x => x.FuelTypeId).NotEmpty().WithMessage("FuelTypeId is not empty");
            RuleFor(x => x.FrontId).NotEmpty().WithMessage("FrontId is not empty");
            RuleFor(x => x.SituationId).NotEmpty().WithMessage("SituationId is not empty");
            //RuleFor(x => x.PhoneNumber).Matches(new Regex(@"^(0(\d{3})-(\d{3})-(\d{2})-(\d{2}))$"));
        }
    }
}

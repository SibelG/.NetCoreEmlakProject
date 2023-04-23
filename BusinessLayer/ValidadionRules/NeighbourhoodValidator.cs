using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidadionRules
{
    public class NeighbourhoodValidator:AbstractValidator<Neighbourhood>
    {
        public NeighbourhoodValidator() {
            RuleFor(x => x.NeighbourhoodName).NotEmpty().WithMessage("District name is not empty");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("CityId is not empty");
        }
    }
}

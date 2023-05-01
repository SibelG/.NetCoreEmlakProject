using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidadionRules
{
    public class DistrictValidator:AbstractValidator<District>
    {
        public DistrictValidator() {
            RuleFor(x=>x.DistrictName).NotEmpty().WithMessage("District name is not empty");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("CityId is not empty");

        }
    }
}

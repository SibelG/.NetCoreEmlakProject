using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidadionRules
{
    public class CityValidator:AbstractValidator<City>
    {
        public CityValidator() {
            RuleFor(x => x.CityName).NotEmpty().WithMessage("City is not empty");
            RuleFor(x=>x.CityName).MinimumLength(3).WithMessage("Enter minimum 3 character ").MaximumLength(13).WithMessage("Enter.maximum 13 character");

        }

    }
}

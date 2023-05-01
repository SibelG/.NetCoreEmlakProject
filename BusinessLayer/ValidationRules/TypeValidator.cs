using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidadionRules
{
    public class TypeValidator: AbstractValidator<EntityLayer.Entities.Type>
    {
        public TypeValidator() {
            RuleFor(x => x.TypeName).NotEmpty().WithMessage("District name is not empty");
            RuleFor(x => x.SituationId).NotEmpty().WithMessage("CityId is not empty");
        }
    }
}

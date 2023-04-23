using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidadionRules
{
    public class SituationValidator:AbstractValidator<Situation>
    {
        public SituationValidator() {
            RuleFor(x => x.SituationName).NotEmpty().WithMessage("District name is not empty");
           
        }
    }
}

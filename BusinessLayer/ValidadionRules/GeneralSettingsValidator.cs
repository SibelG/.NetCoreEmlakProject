using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidadionRules
{
    public class GeneralSettingsValidator:AbstractValidator<GeneralSettings>
    {
        public GeneralSettingsValidator() {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is not empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is not empty");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber name is not empty");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is not empty");
        }
       
    }
}

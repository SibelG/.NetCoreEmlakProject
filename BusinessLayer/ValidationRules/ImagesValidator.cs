using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidadionRules
{
    public class ImagesValidator:AbstractValidator<Images>
    {
        public ImagesValidator() {
            RuleFor(x => x.ImageName).NotEmpty().WithMessage("District name is not empty");
           
        }    
    }
}

using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProjectImagesValidator : AbstractValidator<ProjectImage>
    {
        public ProjectImagesValidator()
        {
            RuleFor(x => x.ImageName).NotEmpty().WithMessage("Image name is not empty");

        }
    }
}

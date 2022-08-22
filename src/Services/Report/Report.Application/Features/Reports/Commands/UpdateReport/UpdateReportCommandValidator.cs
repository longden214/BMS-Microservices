using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommandValidator : AbstractValidator<UpdateReportCommand>
    {
        public UpdateReportCommandValidator()
        {
            RuleFor(r => r.WorkingDay)
                .NotEmpty().WithMessage("{WorkingDay} is not required.")
                .NotNull();

            RuleFor(r => r.WorkingHour)
                .NotEmpty().WithMessage("{WorkingHour} is not required.");

            RuleFor(r => r.RateValue)
                .NotEmpty().WithMessage("{RateValue} is not required.")
                .GreaterThan(0).WithMessage("{RateValue} should be greater than zero.");

            RuleFor(r => r.Note)
                .NotEmpty().WithMessage("{Note} is not required.");

            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("{UserId} is not required.");

            RuleFor(r => r.ProjectId)
                .NotEmpty().WithMessage("{ProjectId} is not required.");

            RuleFor(r => r.Position)
                .NotEmpty().WithMessage("{Position} is not required.");

            RuleFor(r => r.WorkingType)
                .NotEmpty().WithMessage("{WorkingType} is not required.");

            RuleFor(r => r.Status)
                .NotEmpty().WithMessage("{Status} is not required.");
        }
    }
}

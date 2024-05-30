using Core.Entities.Enums;
using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class RecurrencePatternDtoValidator : AbstractValidator<RecurrencePatternDto>
{
    public RecurrencePatternDtoValidator()
    {
        RuleFor(e => e.EndDate)
            .NotNull().DependentRules(() =>
            {
                RuleFor(e => e.StartDate)
                  .NotEmpty()
                  .GreaterThanOrEqualTo(e => e.EndDate);
            });

        RuleFor(e => e.Frequency)
            .NotEmpty()
            .IsEnumName(typeof(Frequency));

        RuleFor(e => e.Interval)
            .GreaterThanOrEqualTo(1);

        RuleForEach(e => e.ByWeekDay)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(7);

        RuleFor(e => e.WeekOrder)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5);

        RuleFor(e => e.ByMonthDay)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(31);

        RuleFor(e => e.ByMonth)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(12);
    }
}

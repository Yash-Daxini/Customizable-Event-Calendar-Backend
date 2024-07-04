using Core.Entities.Enums;
using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class RecurrencePatternDtoValidator : AbstractValidator<RecurrencePatternDto>
{
    public RecurrencePatternDtoValidator()
    {
        RuleFor(e => e.EndDate)
            .NotNull()
            .NotEmpty();

        RuleFor(e => e.StartDate)
            .NotNull()
            .NotEmpty();

        RuleFor(e => e)
            .Must(e => e.StartDate <= e.EndDate)
            .WithMessage("Start date must greater than end date");

        RuleFor(e => e.Frequency)
            .NotNull()
            .NotEmpty()
            .IsEnumName(typeof(Frequency));

        RuleFor(e => e.Interval)
            .GreaterThanOrEqualTo(1);

        RuleForEach(e => e.ByWeekDay)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(7);

        When(e => e.Frequency != null && (e.Frequency.Equals("Daily") || e.Frequency.Equals("Weekly") || e.Frequency.Equals("None")), () =>
        {
            RuleFor(e => e.ByMonth)
                .Null()
                .WithMessage("For daily and weekly event ByMonth must be null");

            RuleFor(e => e.ByMonthDay)
                .Null()
                .WithMessage("For daily and weekly event ByMonthDay must be null");

            RuleFor(e => e.WeekOrder)
                .Null()
                .WithMessage("For daily and weekly event WeekOrder must be null");
        });

        When(e => e.Frequency != null && e.Frequency.Equals("Monthly"), () =>
        {
            RuleFor(e => e.WeekOrder)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5);

            RuleFor(e => e.ByMonthDay)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(31);

            RuleFor(e => e.ByMonth)
                .Null();
        });
        
        When(e => e.Frequency != null && e.Frequency.Equals("Yearly"), () =>
        {
            RuleFor(e => e.WeekOrder)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5);

            RuleFor(e => e.ByMonthDay)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(31);

            RuleFor(e => e.ByMonth)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(12);
        });
    }
}

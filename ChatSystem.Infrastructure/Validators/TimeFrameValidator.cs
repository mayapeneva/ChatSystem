namespace ChatSystem.Infrastructure.Validators
{
    using ChatSystem.Infrastructure.Models;
    using FluentValidation;
    using System;

    public class TimeFrameValidator : ValidatorBase<TimeFrame>
    {
        public TimeFrameValidator()
        {
            RuleFor(x => x.StartTime)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}

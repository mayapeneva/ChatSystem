namespace ChatSystem.Infrastructure.Validators
{
    using ChatSystem.Infrastructure.Models;
    using FluentValidation;
    using System;

    public class TimeFrameValidator : ValidatorBase<TimeFrame>
    {
        public TimeFrameValidator()
        {
            // TODO: add validation start time to be before end time
            RuleFor(x => x.StartTime)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}

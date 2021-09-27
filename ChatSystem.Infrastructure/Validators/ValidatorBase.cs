namespace ChatSystem.Infrastructure.Validators
{
    using Common;
    using FluentValidation;
    using FluentValidation.Results;
    using System.Collections.Generic;

    public abstract class ValidatorBase<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            if (EqualityComparer<T>.Default.Equals(context.InstanceToValidate, default))
            {
                return new ValidationResult(new ValidationFailure[] { new ValidationFailure(typeof(T).Name, string.Format(Constants.DefaultValidationInstance, typeof(T).Name)) });
            }

            return base.Validate(context);
        }
    }
}

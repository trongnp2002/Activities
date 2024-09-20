using Domain;
using FluentValidation;

namespace Application.UserBooks;

public class UserBookingValidator:AbstractValidator<UserBooking>
{
    public UserBookingValidator()
    {
      RuleFor(x=>x.EndTime).NotEmpty().WithMessage("EndTime is required");
      RuleFor(x=>x.StartTime).NotEmpty().WithMessage("StartTime is required");
      RuleFor(x=>x.Subject).NotEmpty().WithMessage("Subject is required");
    }
}
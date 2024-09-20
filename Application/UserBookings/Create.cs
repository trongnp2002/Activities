using Application.Activities;
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.UserBooks;

public class Create
{
    public class Command : IRequest<Result<UserBooking>>
    {
        public UserBooking UserBooking { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.UserBooking).SetValidator(new UserBookingValidator());

        }
    }

    public class Handler : IRequestHandler<Command, Result<UserBooking>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }
        public async Task<Result<UserBooking>> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.UserBookings.Add(request.UserBooking);
            Console.WriteLine(request.UserBooking.StartTime);
            var result = await _context.SaveChangesAsync() > 0;
            if(!result) return Result<UserBooking>.Failure("Fail to create userBooking");
            return Result<UserBooking>.Success(_context.Entry(request.UserBooking).Entity);
        }
    }
}
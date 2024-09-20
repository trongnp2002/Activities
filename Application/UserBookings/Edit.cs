using Application.Activities;
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.UserBooks;

public class Edit
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
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<UserBooking>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.UserBookings.FindAsync(request.UserBooking.Id);
            _mapper.Map(request.UserBooking, activity);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return Result<UserBooking>.Failure("Fail to edit userBooking");
            return Result<UserBooking>.Success(_context.Entry(request.UserBooking).Entity);
        }
    }
}
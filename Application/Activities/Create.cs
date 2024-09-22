using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result<Activity>>
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());

            }
        }

        public class Handler : IRequestHandler<Command, Result<Activity>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }
            public async Task<Result<Activity>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
               
                var attendee = new ActivityAttendee{
                    AppUser = user,
                    Activity = request.Activity,
                    IsHost = true,
                };
                request.Activity.Attendees.Add(attendee);

                _context.Activities.Add(request.Activity);
                
                var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (!result) return Result<Activity>.Failure("Faild to create activity");
                var newActivity = _context.Entry(request.Activity).Entity;
                return Result<Activity>.Success(newActivity);
            }
        }
    }
}
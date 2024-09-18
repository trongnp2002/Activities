using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Result<Activity>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Activity>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                this._context = context;
            }
            public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity is null)
                    return Result<Activity>.Failure("Activity not found");
                return Result<Activity>.Success(activity);
            }
        }
    }
}
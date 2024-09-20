using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserBooks;

public class List
{
    public class Query : IRequest<Result<List<UserBooking>>> { }

    public class Handler : IRequestHandler<Query, Result<List<UserBooking>>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            this._context = context;
        }
        public async Task<Result<List<UserBooking>>> Handle(Query request, CancellationToken cancellationToken)
        {
              
            var userBookings = await _context.UserBookings.ToListAsync();
            return Result<List<UserBooking>>.Success(userBookings);
        }
    }
}
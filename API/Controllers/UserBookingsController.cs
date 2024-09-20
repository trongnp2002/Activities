using Application.UserBooks;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserBookingsController : BaseApiController
{
    public UserBookingsController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetUserBookings(CancellationToken ct)
    {
        return HandleResult(await Mediator.Send(new List.Query(), ct));
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity(UserBooking command)
    {
        return HandleResult(await Mediator.Send(new Create.Command() { UserBooking = command }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(int id, UserBooking updatedUserBooking)
    {
        updatedUserBooking.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command() { UserBooking = updatedUserBooking }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command() { Id = id }));
    }
}
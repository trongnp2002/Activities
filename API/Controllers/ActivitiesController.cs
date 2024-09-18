using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities(CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new List.Query(), ct));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(Guid id, CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new Details.Query() { Id = id }, ct));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity command)
        {
            return HandleResult(await Mediator.Send(new Create.Command() { Activity = command }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id, Activity updatedActivity)
        {
            updatedActivity.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command() { Activity = updatedActivity }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command() { Id = id }));
        }
    }
}
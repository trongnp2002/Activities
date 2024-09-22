using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController()
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetActivities(CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new List.Query(), ct));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActivity(Guid id, CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new Details.Query() { Id = id }, ct));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity command)
        {
            return HandleResult(await Mediator.Send(new Create.Command() { Activity = command }));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id, Activity updatedActivity)
        {
            updatedActivity.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command() { Activity = updatedActivity }));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command() { Id = id }));
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateAttendance.Command() { Id = id }));
        }
    }
}
using Application.Features.MoneyPotFeature.Command.CreateMoneyPot;
using Application.Features.MoneyPotFeature.Query.GetApplicationUsersMoneyPots;
using Application.Features.MoneyPotFeature.Query.GetMoneyPot;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MoneyPot.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MoneyPotController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

       

        public MoneyPotController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost(template: nameof(CreateMoneyPot), Name = nameof(CreateMoneyPot))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateMoneyPot([FromBody] CreateMoneyPotModel model)
        {

            var creatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.CreatorId = creatorId;

            var command = _mapper.Map<CreateMoneyPotCommand>(model);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateMoneyPot), new { id = result }, command);
        }

        [HttpGet("{id}", Name = nameof(GetMoneyPotLink))]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMoneyPotLink([FromQuery] GetMoneyPotLinkModel model) {
            var query = _mapper.Map<GetMoneyPotLinkQuery>(model);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{ApplicationUserId}", Name = nameof(GetApplicationUsersMoneyPots))]
        [ProducesResponseType(typeof(IEnumerable<ApplicationUsersMoneyPotsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplicationUsersMoneyPots([FromQuery] GetApplicationUsersMoneyPotsModel model)
        {
            var creatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.ApplicationUserId = creatorId;
            var query = _mapper.Map<GetApplicationUsersMoneyPotsQuery>(model);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

using Application.Features.MoneyPotFeature.Command.CreateMoneyPot;
using Application.Features.MoneyPotFeature.Query.GetApplicationUsersMoneyPots;
using Application.Features.MoneyPotFeature.Query.GetMoneyPot;
using Application.Features.MoneyPotFeature.Query.GetMoneyPotTransactions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MoneyPot.Controllers
{
    [Authorize]
    [Route("api/MoneyPot")]
    [ApiController]
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
        public async Task<IActionResult> CreateMoneyPot([FromBody] CreateMoneyPotCommand command)
        {

            var creatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            command.CreatorId = creatorId;

           

            var result = await _mediator.Send(command);

            return Ok(result); 
        }

        [HttpGet("{id}/link", Name = "GetMoneyPotLink")]
        [ProducesResponseType(typeof(MoneyPotModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMoneyPotLink(long id)
        {
            var query = new GetMoneyPotLinkQuery { Id = id }; 
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("user-pots", Name = "GetApplicationUsersMoneyPots")] 
        [ProducesResponseType(typeof(IEnumerable<ApplicationUsersMoneyPotsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplicationUsersMoneyPots()
        {
            var creatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var query = new GetApplicationUsersMoneyPotsQuery { ApplicationUserId = 1 };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}/transactions", Name = "GetMoneyPotWithTransactions")]
        [AllowAnonymous]    
        [ProducesResponseType(typeof(MoneyPotModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMoneyPotWithTransactions(int id)
        {
            var query = new GetMoneyPotTransactionsQuery { Id = id};
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

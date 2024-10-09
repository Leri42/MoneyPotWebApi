using Application.Features.MoneyPotFeature.Command.CreateMoneyPot;
using Application.Features.MoneyPotTransactionFeature.Command.CreateMoneyPotTransaction;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MoneyPot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PaymentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost(template: nameof(CreateMoneyPotTransaction), Name = nameof(CreateMoneyPotTransaction))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateMoneyPotTransaction([FromBody] CreateMoneyPotTransactionModel model)
        {
            var command = _mapper.Map<CreateMoneyPotTransactionCommand>(model);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}

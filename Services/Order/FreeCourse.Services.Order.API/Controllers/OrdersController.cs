using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeCourse.Services.Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _service;

        public OrdersController(IMediator mediator, ISharedIdentityService service)
        {
            _mediator = mediator;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            Response<List<OrderDto>> response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _service.UserId });
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateOrderCommand orderCommand)
        {
            var response = await _mediator.Send(orderCommand);
            return CreateActionResultInstance(response);
        }
    }
}

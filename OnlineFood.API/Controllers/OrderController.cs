using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineFood.API.ViewModels;
using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepositry;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrderController(IOrderRepository orderRepositry,
            IOrderItemRepository orderItemRepository,
            ILogger<OrderController> logger,
            IMapper mapper,
            UserManager<StoreUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _orderRepositry = orderRepositry;
            _orderItemRepository = orderItemRepository;

        }
        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user.UserType == 1)
                return Ok(_orderRepositry.GetAllOrder());
            else
                return Ok(_orderRepositry.GetAllOrderUserId(user.Id));
        }
        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;

                var results = _orderRepositry.GetAllOrderUserId(username);//, includeItems);

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var orderItems = _orderRepositry.GetOrderItemById(id);//.GetOrderById(User.Identity.Name, id);

                if (orderItems != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(orderItems));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            // add it to the db
            try
            {
                if (ModelState.IsValid)
                {

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    var order = new Order() { Number = model.OrderNumber, Date = model.OrderDate, SumTotal = model.SumTotal, UserId = currentUser.Id };

                    _orderRepositry.Insert(order, model.Items.ToList());
                    return Created($"/api/orders/{order.Id}", order);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order: {ex}");
            }

            return BadRequest("Failed to save new order");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var orderItem = _orderRepositry.GetItemById(id);
                if (orderItem == null)
                {
                    return NotFound();
                }
                _orderRepositry.Delete(orderItem);
                var orderItems = _orderRepositry.GetOrderItemById(orderItem.OrderId);
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(orderItems));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete order Item: {ex}");
                return BadRequest("Failed to delete order Item");
            }

        }
        [HttpGet("GetAddress/{id}")]
        public async Task<IActionResult> GetAddress(string id)
        {
            try
            {
                StoreUser user = null;
                if (id != "null")
                {
                    user = await _userManager.FindByIdAsync(id);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {

            try
            {
                var order = _orderRepositry.GetOrderById(id);
                order.approved = true;
                _orderRepositry.Update(order);
                var OrderList = _orderRepositry.GetAllOrder();
                return Ok(OrderList);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ShopeExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return NoContent();
        }
    }
}
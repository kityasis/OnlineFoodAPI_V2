using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingRepository _settingRepositry;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;
        public SettingController(ISettingRepository settingRepositry,
              ILogger<OrderController> logger,
              IMapper mapper,
              UserManager<StoreUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _settingRepositry = settingRepositry;

        }
        [HttpGet("Settings")]
        public IActionResult GetSetting()
        {
            var setting = _settingRepositry.GetSetting();
            return Ok(setting);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserSetting model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var setting = _settingRepositry.GetSetting();
                    if (setting != null)
                    {
                        model.Id = setting.Id;
                        _settingRepositry.Update(model);
                    }
                    else
                    {
                        _settingRepositry.Insert(model);

                    }

                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new setting: {ex}");
            }
            return BadRequest("Failed to save new SubCategory");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UserSetting model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.Id)
            {
                return BadRequest();
            }
            try
            {
                _settingRepositry.Update(model);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Failed to update setting: {ex}");
            }
            return NoContent();
        }

        [HttpGet("IsExistZipCode")]
        public async Task<IActionResult> IsExistZipCode(string zipcode)
        {
            var setting = _settingRepositry.GetSetting();

            if (setting != null)
            {
                if (setting.Pincodes.Split(',').ToList().Contains(zipcode))
                {
                    return Ok(true);
                }
            }
            return Ok(false);
        }
    }
}
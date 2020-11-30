using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineFood.API.ViewModels;
using OnlineFood.Data;
using System;
using System.Threading.Tasks;

namespace OnlineFood.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {


        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;
        //private readonly IIdentityServerInteractionService _interaction;
        //private readonly IClientStore _clientStore;
        //private readonly IAuthenticationSchemeProvider _schemeProvider;
        //private readonly IEventService _events;
        public AccountController(SignInManager<StoreUser> signInManager, UserManager<StoreUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        [AllowAnonymous]
        public IActionResult StatusCheck()
        {
            return Ok("OK");
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new StoreUser()
            {
                UserName = model.Email,
                Email = model.Email,
                Titel = model.Titel,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address1 = model.Address1,
                Address2 = model.Address2,
                PhoneNumber = model.PhoneNumber,
                Zipcode = model.Zipcode,
                City = model.City,
                Country = model.Country,
                AdditionalInformation = model.AdditionalInformation,
                UserType = 0
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception e)
            { }
            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));
            return Created(locationHeader, user);

        }

        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();

        }

    }
}

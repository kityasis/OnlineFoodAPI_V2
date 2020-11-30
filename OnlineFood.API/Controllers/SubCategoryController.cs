using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository _subCategoryRepositry;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;
        public SubCategoryController(ISubCategoryRepository subCategoryRepositry,
              ILogger<OrderController> logger,
              IMapper mapper,
              UserManager<StoreUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _subCategoryRepositry = subCategoryRepositry;

        }
        [HttpGet("SubCategories"), AllowAnonymous]
        public IActionResult GetAllSubCategory()
        {
            var SubCategoryList = _subCategoryRepositry.GetAllSubCategory();
            return Ok(SubCategoryList);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var SubCategory = _subCategoryRepositry.GetSubCategoryById(id);//.GetOrderById(User.Identity.Name, id);

                if (SubCategory != null) return Ok(SubCategory);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubCategory model)
        {
            // add it to the db
            try
            {
                if (ModelState.IsValid)
                {

                    _subCategoryRepositry.Insert(model);
                    return Created($"/api/SubCategory/{model.Id}", model);

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new produce: {ex}");
            }

            return BadRequest("Failed to save new SubCategory");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] SubCategory model, int id)
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
                _subCategoryRepositry.Update(model);
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
        [HttpGet("SubCategoriesDDL/{id}")]
        public IActionResult GetSubCategoriesDDL(int id)
        {
            try
            {
                var category = _subCategoryRepositry.GetAllSubCategory();
                if (id != 0)
                {
                    category = category.Where(x => x.CategoryId == id).ToList();
                }

                return Ok(_mapper.Map<IEnumerable<SubCategory>, IEnumerable<DDLViewModel>>(category));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Category DLL: {ex}");
                return BadRequest("Failed to Category DLL");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var subCategory = _subCategoryRepositry.GetSubCategoryById(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            _subCategoryRepositry.Delete(subCategory);
            return Ok(subCategory);
        }
    }
}
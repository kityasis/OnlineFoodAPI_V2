using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepositry;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;
        public CategoryController(ICategoryRepository CategoryRepositry,
              ILogger<OrderController> logger,
              IMapper mapper,
              UserManager<StoreUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _categoryRepositry = CategoryRepositry;

        }
        [HttpGet("Categories")]
        public IActionResult GetAllCategory()
        {
            var CategoryList = _categoryRepositry.GetAllCategory();
            return Ok(CategoryList);
        }
        public IActionResult GetAllCategory(int pageNo, int pageSize, string sortOrder)
        {
            var skip = (pageNo - 1) * pageSize;
            var take = pageSize;

            var categoryList = _categoryRepositry.GetAllCategory();
            var result = categoryList.Skip(skip).Take(take).ToList();
            return Ok(result);
        }
        [HttpGet("CategoriesDDL"), AllowAnonymous]
        public IActionResult GetCategoriesDDL()
        {
            try
            {
                var category = _categoryRepositry.GetAllCategory();

                return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<DDLViewModel>>(category));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Category DLL: {ex}");
                return BadRequest("Failed to Category DLL");
            }
        }
        [HttpGet("GetCategoryWithSubCategories"), AllowAnonymous]
        public IActionResult GetCategoryWithSubCategories()
        {
            try
            {
                var category = _categoryRepositry.GetCategoriesWithSubCategory().Select(i => new Category
                {
                    Name = i.Name,
                    SubCategory = i.SubCategory.Select(o => new SubCategory { Name = o.Name, Id = o.Id }).ToList()
                });
                return Ok(category);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Category DLL: {ex}");
                return BadRequest("Failed to Category DLL");
            }
        }
        [HttpGet("{id:int}"), Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
        {
            try
            {
                var Category = _categoryRepositry.GetCategoryById(id);//.GetOrderById(User.Identity.Name, id);

                if (Category != null) return Ok(Category);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category model)
        {
            // add it to the db
            try
            {
                if (ModelState.IsValid)
                {

                    _categoryRepositry.Insert(model);
                    return Created($"/api/Category/{model.Id}", model);

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

            return BadRequest("Failed to save new Category");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Category model, int id)
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
                _categoryRepositry.Update(model);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepositry.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepositry.Delete(category);
            return Ok(category);
        }
    }
}
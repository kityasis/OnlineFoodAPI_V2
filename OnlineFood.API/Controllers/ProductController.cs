using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineFood.API.ViewModels;
using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;

namespace OnlineFood.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepositry _productRepositry;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _configuration;

        public ProductController(IProductRepositry productRepositry,
              ILogger<OrderController> logger,
              IMapper mapper,
              UserManager<StoreUser> userManager,
              IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _productRepositry = productRepositry;
            _configuration = configuration;

        }
        [HttpGet("Products")]
        public IActionResult GetAllProduct()
        {
            var ProductList = _productRepositry.GetAllProduct();
            return Ok(ProductList);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _productRepositry.GetProductById(id);//.GetOrderById(User.Identity.Name, id);               
                if (product != null)
                    return Ok(_mapper.Map<Product, ProductViewModel>(product));
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product model)
        {
            // add it to the db
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ReleaseDate == DateTime.MinValue)
                    {
                        model.ReleaseDate = DateTime.Now;
                    }
                    _productRepositry.Insert(model);
                    return Created($"/api/Product/{model.Id}", model);

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

            return BadRequest("Failed to save new Product");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Product model, int id)
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
                _productRepositry.Update(model);
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
        [HttpPost("Upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    using (var client = new AmazonS3Client(_configuration["AWS:aws_access_key_id"], _configuration["AWS:aws_secret_access_key"], RegionEndpoint.APSouth1))
                    {
                        using (var newMemoryStream = new MemoryStream())
                        {
                            file.CopyTo(newMemoryStream);
                            var uploadRequest = new TransferUtilityUploadRequest
                            {
                                InputStream = newMemoryStream,
                                Key = file.FileName,
                                BucketName = _configuration["AWS:bucket_name"],
                                CannedACL = S3CannedACL.PublicRead
                            };
                            var fileTransferUtility = new TransferUtility(client);
                            await fileTransferUtility.UploadAsync(uploadRequest);
                            var dbPath = $"http://{_configuration["AWS:bucket_name"]}.s3.amazonaws.com/{file.FileName}";
                            return Ok(new { dbPath });
                        }
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

            //try
            //{
            //    var file = Request.Form.Files[0];
            //    var folderName = Path.Combine("Resources", "Images");
            //    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


            //    if (file.Length > 0)
            //    {
            //        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //        var fullPath = Path.Combine(pathToSave, fileName);
            //        var dbPath = Path.Combine(folderName, fileName);

            //        using (var stream = new FileStream(fullPath, FileMode.Create))
            //        {
            //            await file.CopyToAsync(stream);
            //        }

            //        return Ok(new { dbPath });
            //    }
            //    else
            //    {
            //        return BadRequest();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Internal server error: {ex}");
            //}
        }
        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _productRepositry.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepositry.Delete(product);
            return Ok(product);
        }
    }
}
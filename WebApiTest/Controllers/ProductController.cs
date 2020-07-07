using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiTest;
using WebApplicationTest.Models;
using WebApplicationTest.Services;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private ILogger _logger;
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _environment;

        public ProductController(ILogger<ProductController> logger, IProductService service, IWebHostEnvironment environment)
        {
            _logger = logger;
            _service = service; //dependency injection
            _environment = environment;
        }

        //======================================================| Get all objects
        [HttpGet("/api/products")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var response = _service.GetAllProducts();
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        /*[Route("api/get/list/tada")]
        [HttpGet]
        public IHttpActionResult GetAllTada()
        {
            List<MRK_TADA> result = tadaService.GetAllObjects();
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }*/

        //======================================================| Get object by Id
        // GET: api/Products/5
        [HttpGet("/api/products/{id}", Name = "GetProductById")]
        public ActionResult<Product> GetProductById([FromRoute] int id)
        {
            var response = _service.GetProductById(id);
            if(response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        // OR
        [HttpGet("/api/product/{id}")]
        public async Task<IActionResult> GetProductById2(int id)
        {
            var response = await _service.GetProductById2(id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        //======================================================| Add
        [HttpPost("/api/products")]
        public ActionResult<Product> AddProduct([FromBody] Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                var response = _service.AddProduct(item);
                if (response != null)
                {
                    //return Ok(response);
                    return CreatedAtRoute(nameof(GetProductById), new { Id = response.ID }, response); //Get model after creating
                }
                return NotFound();
            }
        }

        //======================================================| Put/Update
        [HttpPut("/api/products/{id}")]
        public ActionResult<Product> UpdateProduct([FromQuery] string id, [FromBody] Product item)
        {
            var response = _service.UpdateProduct(item);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        //======================================================| Delete
        [HttpDelete("/api/products/{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var response = _service.DeleteProduct(id);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();

        }

        //======================================================| Image upload
        [HttpPost("/api/upload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var imgPath = @"\upload\images\";
                var uploadPath = _environment.WebRootPath + imgPath;
                //Create Directory
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                //Create unique file name
                var fileName = Path.GetFileName(Guid.NewGuid().ToString() + "." + file.FileName.Split(".")[1].ToLower());
                var filePath = @".." + Path.Combine(imgPath + @"\", fileName);
                using (var fileStream = new FileStream(uploadPath + fileName, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                //ViewData["FileLocation"] = filePath;
                return Ok(filePath);
            }
            return BadRequest();
        }

        [HttpPost("/api/img/upload")]
        public async Task<IActionResult> ImgUpload([FromForm] IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var path = _environment.WebRootPath + "\\upload\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream stream = System.IO.File.Create(path + file.FileName))
                    {
                        await file.CopyToAsync(stream);
                        //file.CopyTo(stream);
                        stream.Flush();
                        return Ok("\\upload\\" + file.FileName);
                    }
                }
                return BadRequest();
            }
            catch (Exception e)
            {

                return NotFound(e.Message.ToString());
            }
        }

        //======================================================| Index
        [HttpPost("/api/index")]
        public ActionResult<String> Index()
        {
            var _items = new List<Product>
            {
                new Product{Name="Nokia 730 Dual", Brand="Nokia"},
                new Product{Name="iPhone 6s", Brand="Apple"},
                new Product{Name="Galaxy S6", Brand="Samsung"}
            };

            var response = _service.Index(_items);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        //======================================================| Weather
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("/api/weathers")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.Project.Business.Service.Abstraction.Mongo;
using Utility.Project.Core.ApiController;
using Utility.Project.Model.Document;

namespace Utility.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return ApiResponse<Product>(_productService.FindByObjectId(id));
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return ApiResponse<List<Product>>(_productService.GettAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            return ApiResponse(_productService.Add(product));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            return ApiResponse(_productService.Update(product));
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] Product product)
        {
            return ApiResponse(_productService.Patch(product));
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Product product)
        {
            return ApiResponse(_productService.Delete(product));
        }

        [HttpPost("insertMany")]
        public IActionResult InsertMany([FromBody] List<Product> product)
        {
            _productService.InsertMany(product);
            return StatusCode((int)HttpStatusCode.Accepted);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task1.Model;
using task1.Repositories;

namespace task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }


        [HttpGet("{id:int}")] 
        public IActionResult GetById(int id)
        {
          Product product = productRepository.GetById(id);
            if(product != null) 
               return Ok(product);

            return BadRequest("invalid id");            
        }


            [HttpGet]
       // [Route("api/products")]
        public IActionResult GetAll() 
            {
                List<Product> products = productRepository.GetAll();
                return Ok(products);
            }

        [HttpPost]
        public IActionResult addProduct(Product product)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    productRepository.addProduct(product);
                    productRepository.save();
                    return CreatedAtAction("GetById", new { id = product.Id }, product); // how get id >> 0 ????
                }
                catch (Exception ex) 
                {
                    return BadRequest(ex.Message);
                }
              }
            return BadRequest(); 
           
        }




        [HttpPut]
        public IActionResult updateProduct(int id , Product product)             
        {
           Product pro = productRepository.GetById(id);
            if(pro != null && product.Id == id)
            {
                try
                {
                    productRepository.updateProduct(product);  // select of update use dif context so 2 dif context try to track same obj ??????   // same obj cannot be tracked by 2 references
                    productRepository.save();
                    return NoContent();                // returns should be memorized for each action????? no
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Invalid Id");
        }


        [HttpDelete]
        public IActionResult deleteProduct(int id) 
        {
            try
            {
                productRepository.deleteProduct(id);
                productRepository.save();
                return NoContent();
            }
            catch (Exception ex) 
            {
              return BadRequest(ex.Message);
            }
         
        }
    }
}

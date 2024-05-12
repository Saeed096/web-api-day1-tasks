using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task1.DTO;
using task1.Model;
using task1.Repositories;

namespace task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository _categoryRepository) 
        {
            categoryRepository = _categoryRepository;
        }
       

        [HttpPost]
        public IActionResult addCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categoryRepository.addCategory(category);
                    categoryRepository.save();
                    return CreatedAtAction("GetById", new { id = category.Id }, category); // how get id >> 0 ????
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();

        }

/**/        //[HttpGet]
        //public ActionResult Get(dynamic filteringParam) 
        //{
        //     categoryRepository.Get(filteringParam);
        //}

        [HttpGet]
        [Authorize] 
        public ActionResult GetById(int id)    // send as query string  >> if route attr >> send as segment >> any comp.. send in body except u said [fromhead] or from.....
        {
           Category c = categoryRepository.Get(c=>c.Id == id , categoryInclude.products); 
            if(c != null)
            {
                CategoryWithProductsPTO categoryWithProductsPTO = new CategoryWithProductsPTO();

                foreach(Product product in c.Products)
                {
                    categoryWithProductsPTO.productsNames.Add(product.name);
                }

                categoryWithProductsPTO.Name = c.Name;
                categoryWithProductsPTO.Id = c.Id;
                return Ok(categoryWithProductsPTO);
            }

            return BadRequest("Invalid Id");
        }
    }
}

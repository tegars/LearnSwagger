using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnSwagger.EntityFramework;
using LearnSwagger.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnLazyLoader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private DBContext _context;
        public CategoriesController(DBContext context)
        {
            _context = context;
        }
        // GET: api/Categories
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = _context.Categories.ToList();
                List<CategoryDto> categoriesDto = new List<CategoryDto>();
                foreach (var category in categories)
                {
                    var categoryDto = new CategoryDto();
                    categoryDto.Id = category.Id;
                    categoryDto.Name = category.Name;

                    foreach (var product in category.Products)
                    {
                        var productDto = new ProductDto();
                        productDto.Id = product.Id;
                        productDto.CategoryId = product.CategoryId;
                        productDto.Name = product.Name;
                        productDto.Colour = product.Colour;
                        productDto.Price = product.Price;
                        categoryDto.Products.Add(productDto);
                    }
                    categoriesDto.Add(categoryDto);
                }
                return Ok(categoriesDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
                var categoryDto = new CategoryDto();
                categoryDto.Id = category.Id;
                categoryDto.Name = category.Name;
                foreach (var product in category.Products)
                {
                    var productDto = new ProductDto();
                    productDto.Id = product.Id;
                    productDto.CategoryId = product.CategoryId;
                    productDto.Name = product.Name;
                    productDto.Colour = product.Colour;
                    productDto.Price = product.Price;
                    categoryDto.Products.Add(productDto);
                }
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //// POST: api/Categories
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Categories/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnSwagger.EntityFramework;
using LearnSwagger.EntityFramework.Entities;
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

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> Post(CategoryDto categoryDto)
        {
            try{
                var category = new Category();
                category.Name = categoryDto.Name;
                _context.Categories.Add(category);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryDto categoryDto)
        {
            try
            {
                var category = _context.Categories.Where(x=>x.Id == id).FirstOrDefault();
                if (category != null)
                {
                    category.Name = categoryDto.Name;
                    _context.SaveChanges();
                    return Ok();
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return Ok();
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}

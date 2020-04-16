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
    public class ProductsController : ControllerBase
    {
        private DBContext _context;
        public ProductsController(DBContext context)
        {
            _context = context;
        }
        public IActionResult Get()
        {
            List<Product> products = _context.Products.ToList();
            List<ProductDto> productsDto = new List<ProductDto>();
            foreach (var product in products)
            {
                var productDto = new ProductDto();
                productDto.Id = product.Id;
                productDto.CategoryId = product.CategoryId;
                productDto.Name = product.Name;
                productDto.Colour = product.Colour;
                productDto.Price = product.Price;
                productDto.Category.Id = product.Category.Id;
                productDto.Category.Name = product.Category.Name;
                productsDto.Add(productDto);
            }
            return Ok(productsDto);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnSwagger.Model
{
    public class ProductDto
    {
        public int Id { set; get; }
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string Colour { set; get; }
        public string Price { set; get; }
        public CategoryDto Category { set; get; }
        public ProductDto()
        {
            Category = new CategoryDto();
        }
    }
}

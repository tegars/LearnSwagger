using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnSwagger.Model
{
    public class CategoryDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public List<ProductDto> Products { get; set; }
        public CategoryDto()
        {
            Products = new List<ProductDto>();
        }
    }
}

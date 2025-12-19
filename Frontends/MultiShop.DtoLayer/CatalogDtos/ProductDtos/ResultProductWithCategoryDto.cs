using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.CatalogDtos.ProductDtos
{
    public class ResultProductWithCategoryDto
    {
        public ResultProductDto resultProductDtos { get; set; }
        public ResultCategoryDto resultCategoryDtos { get; set; }
    }
}

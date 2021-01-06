using Rocky.Application.Dtos.Category;
using Rocky.Application.Dtos.Product;
using System.Collections.Generic;

namespace Rocky.Application.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<ProductGetDto> Products { get; set; }
        public IEnumerable<CategoryGetDto> Categories { get; set; }
    }
}

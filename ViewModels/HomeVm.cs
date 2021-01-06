using Rocky.Dto.Category;
using Rocky.Dto.Product;
using System.Collections.Generic;

namespace Rocky.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<ProductGetDto> Products { get; set; }
        public IEnumerable<CategoryGetDto> Categories { get; set; }
    }
}

using System.Collections.Generic;

namespace Rocky.Application.ViewModels
{
    public class ProductUserVm
    {
        public ProductUserVm()
        {
            ProductList = new List<Product>();
        }

        public ApplicationUser ApplicationUser { get; set; }
        public IList<Product> ProductList { get; set; }
    }
}

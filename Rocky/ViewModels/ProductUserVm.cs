using System.Collections.Generic;
using Rocky.Domain.Entities;

namespace Rocky.ViewModels
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

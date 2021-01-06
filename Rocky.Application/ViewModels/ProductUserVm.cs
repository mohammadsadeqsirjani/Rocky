using Rocky.Application.Dtos.Product;
using Rocky.Domain.Entities;
using System.Collections.Generic;

namespace Rocky.Application.ViewModels
{
    public class ProductUserVm
    {
        public ProductUserVm()
        {
            Products = new List<ProductGetDto>();
        }

        public ApplicationUser ApplicationUser { get; set; }
        public IList<ProductGetDto> Products { get; set; }
    }
}

using Rocky.Application.ViewModels.Dtos.Product;
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
        public List<ProductGetDto> Products { get; set; }
    }
}

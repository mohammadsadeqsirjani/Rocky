using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky.Application.ViewModels.Dtos.Product;
using System.Collections.Generic;

namespace Rocky.Application.ViewModels
{
    public class ProductVm
    {
        public ProductUpsertDto Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> ApplicationTypes { get; set; }
    }
}

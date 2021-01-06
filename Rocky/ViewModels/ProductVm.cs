using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky.Dto.Product;
using System.Collections.Generic;

namespace Rocky.ViewModels
{
    public class ProductVm
    {
        public ProductUpsertDto Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> ApplicationTypes { get; set; }
    }
}

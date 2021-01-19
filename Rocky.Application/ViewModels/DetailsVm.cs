using Rocky.Application.ViewModels.Dtos.Product;
using System.ComponentModel;

namespace Rocky.Application.ViewModels
{
    public class DetailsVm
    {
        public DetailsVm()
        {
            SqFt = 1;
            Product = new ProductGetDto();
        }

        public ProductGetDto Product { get; set; }
        [DefaultValue(1)]
        public int SqFt { get; set; } = 1;
        public bool ExistsInCart { get; set; }
    }
}

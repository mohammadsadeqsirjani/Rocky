using Rocky.Application.Dtos.Product;

namespace Rocky.Application.ViewModels
{
    public class DetailsVm
    {
        public DetailsVm()
        {
            Product = new ProductGetDto();
        }

        public ProductGetDto Product { get; set; }
        public bool ExistsInCart { get; set; }
    }
}

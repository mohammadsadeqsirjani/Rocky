using Rocky.Dto.Product;

namespace Rocky.ViewModels
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

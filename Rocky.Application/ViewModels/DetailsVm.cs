using Rocky.Application.ViewModels.Dtos.Product;

namespace Rocky.Application.ViewModels
{
    public class DetailsVm
    {
        public DetailsVm()
        {
            Product = new ProductGetDto();
        }

        public ProductGetDto Product { get; set; }
        public int SqFt { get; set; } = 1;
        public bool ExistsInCart { get; set; }
    }
}

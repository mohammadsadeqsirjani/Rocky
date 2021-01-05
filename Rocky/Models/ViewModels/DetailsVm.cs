namespace Rocky.Models.ViewModels
{
    public class DetailsVm
    {
        public DetailsVm()
        {
            Product = new Product();
        }

        public Product Product { get; set; }
        public bool ExistsInCart { get; set; }
    }
}

using Rocky.Application.ViewModels.Dtos.Common;

namespace Rocky.Application.ViewModels.Dtos.Product
{
    public class ProductUpsertDto : EntityEditDto
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public int CategoryId { get; set; }
        public int ApplicationTypeId { get; set; }
    }
}

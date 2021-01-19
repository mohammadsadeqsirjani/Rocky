using Rocky.Application.ViewModels.Dtos.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rocky.Application.ViewModels.Dtos.Product
{
    public class ProductGetDto : EntityGetDto
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public int CategoryId { get; set; }
        public int ApplicationTypeId { get; set; }

        [Range(1, 100)]
        public int Sqft { get; set; }

        [DisplayName("Caegogry Type")]
        public string CategoryType { get; set; }

        [DisplayName("Application Type")]
        public string ApplicationType { get; set; }

    }
}

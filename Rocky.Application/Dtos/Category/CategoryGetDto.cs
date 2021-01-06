using Rocky.Application.Dtos.Common;
using System.ComponentModel;

namespace Rocky.Application.Dtos.Category
{
    public class CategoryGetDto : EntityGetDto
    {
        public string Name { get; set; }
        [DisplayName("Display Name")]
        public int DisplayOrder { get; set; }
    }
}

using Rocky.Dto.Common;
using System.ComponentModel;

namespace Rocky.Dto.Category
{
    public class CategoryGetDto : EntityGetDto
    {
        public string Name { get; set; }
        [DisplayName("Display Name")]
        public int DisplayOrder { get; set; }
    }
}

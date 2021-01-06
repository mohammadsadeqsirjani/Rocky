using Rocky.Application.Dtos.Common;

namespace Rocky.Application.Dtos.Category
{
    public class CategoryEditDto : EntityEditDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}

using Rocky.Dto.Common;

namespace Rocky.Dto.Category
{
    public class CategoryEditDto : EntityEditDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}

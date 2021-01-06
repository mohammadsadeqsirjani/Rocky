using Rocky.Dto.Common;

namespace Rocky.Dto.Category
{
    public class CategoryAddDto : EntityAddDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}

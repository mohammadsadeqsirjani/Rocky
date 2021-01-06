using Rocky.Application.Dtos.Common;

namespace Rocky.Application.Dtos.Category
{
    public class CategoryAddDto : EntityAddDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}

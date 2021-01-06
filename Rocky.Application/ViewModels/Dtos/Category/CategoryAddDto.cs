using Rocky.Application.ViewModels.Dtos.Common;

namespace Rocky.Application.ViewModels.Dtos.Category
{
    public class CategoryAddDto : EntityAddDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}

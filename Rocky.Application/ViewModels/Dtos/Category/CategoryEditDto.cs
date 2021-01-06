using Rocky.Application.ViewModels.Dtos.Common;

namespace Rocky.Application.ViewModels.Dtos.Category
{
    public class CategoryEditDto : EntityEditDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}

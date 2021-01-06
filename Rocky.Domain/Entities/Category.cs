using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Rocky.Domain.Common;

namespace Rocky.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

        public List<Product> Products { get; set; }
    }
}

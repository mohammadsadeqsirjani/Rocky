using Rocky.Models.Common;
using System.Collections.Generic;

namespace Rocky.Models
{
    public class ApplicationType : BaseEntity
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}

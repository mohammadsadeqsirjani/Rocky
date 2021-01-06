using System.Collections.Generic;
using Rocky.Domain.Common;

namespace Rocky.Domain.Entities
{
    public class ApplicationType : BaseEntity
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}

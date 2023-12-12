using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concretes
{
    public class Category : Entity<Guid>
    {
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}

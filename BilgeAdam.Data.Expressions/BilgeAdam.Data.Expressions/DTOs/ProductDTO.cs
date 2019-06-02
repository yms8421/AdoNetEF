using System;

namespace BilgeAdam.Data.Expressions.DTOs
{
    class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int? Stock { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop
{
    public  class CartItem
    {
            [Key]
            public int Id { get; set; }
            public int Count { get; set; }
            public int ProductId { get; set; }
            public Product product { get; set; }
            public ShoppingCart ShoppingCart { get; set; }
        
    }
}

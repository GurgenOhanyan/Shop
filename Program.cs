using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OnlineShop
{
    public class Program
    {

        static void Main(string[] args)
        {
            //AddProducts();
            //var watermelon = new Product()
            //{
            //    Name = "watermelon",
            //    Price = 1500,
            //    Quantity = 500,
            //    Category = ProductCategory.Fruit
            //};
            //Add(watermelon);
            //DeleteProductFromTable("orange");
            //UpdateProduct("kitkat", 600, 220);
            // Console.WriteLine(GetProduct(4));
            AddProductIntoShopingCart("kitkat",3);

        }
        //public static void AddProducts()
        //{
        //    var orange = new Product()
        //    {
        //        Name = "orange",
        //        Price = 800,
        //        Quantity = 1000,
        //        Category = ProductCategory.Fruit
        //    };
        //    var cheese = new Product()
        //    {
        //        Name = "cheese",
        //        Price = 1600,
        //        Quantity = 50,
        //        Category = ProductCategory.Dairy
        //    };
        //    var candy = new Product()
        //    {
        //        Name = "kitkat",
        //        Price = 250,
        //        Quantity = 2000,
        //        Category = ProductCategory.Sweet
        //    };
        //    using (OnlineShopContext dbContext = new OnlineShopContext())
        //    {
        //        dbContext.products.Add(orange);
        //        dbContext.products.Add(cheese);
        //        dbContext.products.Add(candy);
        //        dbContext.SaveChanges();
        //    }

        //}
        public static IEnumerable<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (OnlineShopContext dbContext = new OnlineShopContext())
            {
                products = dbContext.products.ToList();
            }
            return products;
        }

        static void Add(Product product)
        {
            using (OnlineShopContext dbContext = new OnlineShopContext())
            {
                var allproducts = GetAllProducts();
                foreach (var pd in allproducts)
                {
                    if (String.Equals(pd.Name,product.Name))
                    {

                        Product temp_product = new Product();
                        temp_product = dbContext.products.Find(pd.Id);
                        temp_product.Quantity += product.Quantity;
                        dbContext.SaveChanges();
                        return ;
                    }
                }
                dbContext.products.Add(product);
                dbContext.SaveChanges();
            }
        }
        public static void DeleteProductFromTable(string productName)
        {
            using (OnlineShopContext dbcontext = new OnlineShopContext())
            {
                var allProducts = GetAllProducts();
                foreach (var product in allProducts)
                {
                    if (String.Equals(product.Name,productName))
                    {
                        dbcontext.products.Remove(product);
                        dbcontext.SaveChanges();
                    }

                }
            }
        }
        public static void UpdateProduct(string ProductName,int quantity,int price)
        {
            var allProducts = GetAllProducts();
            using (OnlineShopContext dbContext = new OnlineShopContext())
            {
                foreach (var product in allProducts)
                {
                    if (String.Equals(product.Name, ProductName)) 
                    {
                        Product product1 = new Product();
                        product1 = dbContext.products.Find(product.Id);
                        product1.Quantity = quantity;
                        product1.Price = price;
                        dbContext.SaveChanges();
                    }
                }
            
            }

        }
        public static Product GetProduct(int id)
        {
            var allProducts = GetAllProducts();
            Product product1 = new Product();
            foreach (var product in allProducts)
            {
                if (product.Id == id)
                {
                    product1 = product;
                }
            }
            return product1;
        }
        public static ICollection<CartItem> GetAllCartItems()
        {
            List<CartItem> cartItems = new List<CartItem>();
            using (OnlineShopContext dbContext = new OnlineShopContext())
            {
                var items = dbContext.cartItems.ToList();
                foreach (var item in items)
                {
                    cartItems.Add(item);
                }
            }
            return cartItems;
        }

        public static void AddProductIntoShopingCart(string productname,int productCount)
        {
            var products = GetAllProducts();
            using (OnlineShopContext dbContext = new OnlineShopContext())
            {
                CartItem cartItem1 = new CartItem();
                ShoppingCart shoppingCart1 = new ShoppingCart();
                foreach (var product in products)
                {
                    if (String.Equals(productname, product.Name))
                    {
                      CartItem cartItem = new CartItem()
                      {
                          Count=productCount,
                          ProductId=product.Id,
                      };
                      ShoppingCart shoppingCart = new ShoppingCart()
                      {
                            //CartItems = GetAllCartItems(),
                            CreatedOn=DateTime.Now,
                            TotalPrice = product.Price * productCount,
                            ItemsCount=productCount,
                      };
                        shoppingCart1 = shoppingCart;
                        cartItem1 = cartItem;
                    }
                }
                
                dbContext.cartItems.Add(cartItem1);
                dbContext.shopingCarts.Add(shoppingCart1);
                dbContext.SaveChanges();
            }
        }
    }
}

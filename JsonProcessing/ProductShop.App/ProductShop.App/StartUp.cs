namespace ProductShop.App
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;

    using Data;
    using Models;
    using Newtonsoft.Json;
    using ProductShop.App.Dto;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

            //RestDatabase(context);
            //ImportUsers(context, mapper);
            //ImportProducts(context, mapper);
            //ImportCategories(context);
            //ImportCategoryProduct(context);
            //ExportProductsInRange(context);
            //ExportUsersSoldProducts(context);
            //ExportCategoriesByProducts(context);
            ExportUsersAndProducts(context);
        }

        private static void ExportUsersAndProducts(ProductShopContext context)
        {
            var users = context.Users.Where(u => u.ProductsSold.Count > 0)
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(x => new
                {
                    usersCount = context.Users.Where(u => u.ProductsSold.Count > 0).Count(),
                    users = context.Users.Where(u => u.ProductsSold.Count > 0)
                                         .Select(us => new
                                         {
                                             firstName = us.FirstName,
                                             lastName = us.LastName,
                                             age = us.Age,      
                                             soldProducts = new
                                             {
                                                 count = us.ProductsSold.Count,
                                                 products = us.ProductsSold.Select(pro => new
                                                 {
                                                     name = pro.Name,
                                                     price = pro.Price
                                                 })
                                             }
                                         }).ToArray()

                }).ToArray();

            var serializer = JsonConvert.SerializeObject(users, Formatting.Indented);

            File.WriteAllText(@"..\..\..\Json/users-and-products.jsonn", serializer);
        }

        private static void ExportCategoriesByProducts(ProductShopContext context)
        {
            var categories = context.Categories.OrderBy(x => x.CategoryProducts.Count)
                .Select(x => new
                {
                    category = x.Name,
                    productCount = x.CategoryProducts.Count(),
                    averagePrice = x.CategoryProducts.Sum(p => p.Product.Price) / x.CategoryProducts.Count,
                    totalRevenue = x.CategoryProducts.Sum(p => p.Product.Price)
                }).ToArray();

            var serializer = JsonConvert.SerializeObject(categories, Formatting.Indented);

            File.WriteAllText(@"..\..\..\Json/categories-by-products.json", serializer);
        }

        private static void ExportUsersSoldProducts(ProductShopContext context)
        {
            var products = context.Users.Where(x => x.ProductsSold.Count > 0)
                .OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    soldProducts = x.ProductsSold.Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName
                    }).ToArray()
                }).ToArray();

            var serializer = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText(@"..\..\..\Json/users-sold-products.json", serializer);
        }

        private static void ExportProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName ?? x.Seller.LastName
                })
                .ToArray();

            var jsonProducts = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText(@"..\..\..\Json/products-in-range.json", jsonProducts);
        }

        private static void ImportCategoryProduct(ProductShopContext context)
        {
            var categoryProducts = new List<CategoryProduct>();
            for (int productId = 1; productId < 201; productId++)
            {
                var categoryId = new Random().Next(1, 12);
                var categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryId,
                    ProductId = productId
                };
                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        private static void ImportCategories(ProductShopContext context)
        {
            string jsonString = @"..\..\..\Json/categories.json";
            string categoryJson = File.ReadAllText(jsonString);

            var deserializedUsers = JsonConvert.DeserializeObject<Category[]>(categoryJson);

            var categories = new List<Category>();
            foreach (var category in deserializedUsers)
            {
                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

        }

        private static void ImportProducts(ProductShopContext context, IMapper mapper)
        {
            string jsonString = @"..\..\..\Json/products.json";
            string usersJson = File.ReadAllText(jsonString);

            var deserializedProducts = JsonConvert.DeserializeObject<ProductDto[]>(usersJson);

            var products = new List<Product>();

            var counter = 1;
            foreach (var productDto in deserializedProducts)
            {
                var sellersId = new Random().Next(1, 31);
                var buyerId = new Random().Next(32, 57);

                var product = mapper.Map<Product>(productDto);
                product.SellerId = sellersId;

                if (counter == 3)
                {
                    counter = 1;
                    products.Add(product);
                    continue;
                }

                counter++;
                product.BuyerId = buyerId;
                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsers(ProductShopContext context, IMapper mapper)
        {
            string jsonString = @"..\..\..\Json/users.json";
            string usersJson = File.ReadAllText(jsonString);

            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(usersJson);

            var users = new List<User>();
            foreach (var userDto in deserializedUsers)
            {
                var user = mapper.Map<User>(userDto);

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static void RestDatabase(ProductShopContext context)
        {
            Console.WriteLine("Reset Database...");
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}

namespace ProductShop.Client
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using ProductShop.Client.Dtos;
    using ProductShop.Data;
    using ProductShop.Models;
    using System;
    using System.Collections.Generic;
    using DataAnotations = System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using ProductShop.Client.Dtos.Export;
    using System.Text;
    using System.Xml;
    using ProductShop.Client.Dtos.Query4Dtos;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new ProductShopContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();

            ImportQuery4ProductsCategories(context);
        }

        private static void ImportQuery4ProductsCategories(ProductShopContext context)
        {
            var users = new UsersDtoQ4
            {
                Count = context.Users.Count(),
                Users = context.Users.Select(x => new UserDtoQ4
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age.ToString(),
                    ProductsSold = x.SoldProducts.Select(s => new SoldProductQ4
                    {
                        Count = x.SoldProducts.Count(),
                        ProductDto = x.SoldProducts.Select(k => new ProductDtoQ4
                        {
                            Name = k.Name,
                            Price = k.Price 
                        }).ToArray()  
                    }).ToArray()
                }).ToArray()
            } ;

            var xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(UsersDtoQ4), new XmlRootAttribute("users"));
            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), users, xmlNameSpaces);

            File.WriteAllText("../../../XmlFiles/users-and-products.xml", sb.ToString());
        }

        private static void ImportCategoryByProductXml(ProductShopContext context)
        {
            var categories = context.Categories
                                    .OrderByDescending(x => x.CategoryProducts.Count)
                                    .Select(c => new CategoryDtoExp
                                    {
                                        Name = c.Name,
                                        Count = c.CategoryProducts.Count,
                                        TotalRevenue = c.CategoryProducts.Sum(x => x.Product.Price),
                                        AveragePrice = c.CategoryProducts.Average(x => x.Product.Price)
                                    }).ToArray();

            var xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(CategoryDtoExp[]), new XmlRootAttribute("products"));
            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), categories, xmlNameSpaces);

            File.WriteAllText("../../../XmlFiles/categories-by-products.xml", sb.ToString());
        }

        private static void ImportXmlUsers(ProductShopContext context)
        {
            var users = context.Users.Where(x => x.SoldProducts.Count > 1)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new UserDtoExp
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.SoldProducts.Select(s => new SoldProduct
                    {
                        Name = s.Name,
                        Price = s.Price
                    }).ToArray()
                }).ToArray();

            var sb = new StringBuilder();
            var xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(UserDtoExp[]), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, xmlNameSpaces);

            File.WriteAllText("../../../XmlFiles/users-sold-products.xml", sb.ToString());
        }

        private static void ImportXmlProducts(ProductShopContext context)
        {
            var products = context.Products.Where(p => p.Price >= 100 && p.Price <= 2000 && p.Buyer != null)
                 .OrderByDescending(x => x.Price)
                 .Select(x => new ProductDtoExp
                 {
                     Name = x.Name,
                     Price = x.Price,
                     Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName ?? x.Buyer.LastName
                 }).ToArray();

            var xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(ProductDtoExp[]), new XmlRootAttribute("products"));
            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), products, xmlNameSpaces);

            File.WriteAllText("../../../XmlFiles/products-in-range.xml", sb.ToString());
        }

        private static void InitializeCategoriesAndProducts(ProductShopContext context)
        {
            var categoryProducts = new List<CategoryProducts>();
            for (int productId = 1; productId < 201; productId++)
            {
                var categoryId = new Random().Next(1, 12);

                var categoryProduct = new CategoryProducts()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };

                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        private static void ImportCategoriesFromXml(ProductShopContext context, IMapper mapper)
        {
            var xmlString = File.ReadAllText("../../../XmlFiles/categories.xml");

            var serializer = new XmlSerializer(typeof(CategoriesDto[]), new XmlRootAttribute("categories"));
            var deserialize = (CategoriesDto[])serializer.Deserialize(new StringReader(xmlString));

            var categories = new List<Category>();
            foreach (var catDto in deserialize)
            {
                if (!IsValidUserDto(catDto))
                {
                    continue;
                }

                var category = mapper.Map<Category>(catDto);
                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportProductsFromXml(ProductShopContext context, IMapper mapper)
        {
            var xmlString = File.ReadAllText("../../../XmlFiles/products.xml");

            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            var deserialize = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));

            var products = new List<Product>();
            int counter = 1;
            foreach (var productDto in deserialize)
            {
                if (!IsValidUserDto(productDto))
                {
                    continue;
                }
                var product = mapper.Map<Product>(productDto);

                var buyerId = new Random().Next(1, 30);
                var sellerId = new Random().Next(1, 57);

                product.BuyerId = buyerId;
                product.SellerId = sellerId;

                if (counter == 6)
                {
                    product.BuyerId = null;
                    counter = 0;
                }

                products.Add(product);
                counter++;
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsersFromXml(ProductShopContext context, IMapper mapper)
        {
            var xmlString = File.ReadAllText("../../../XmlFiles/users.xml");

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            var deserialize = (UserDto[])serializer.Deserialize(new StringReader(xmlString));

            var users = new List<User>();
            foreach (var userDto in deserialize)
            {
                if (!IsValidUserDto(userDto))
                {
                    continue;
                }

                var user = mapper.Map<User>(userDto);

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        public static bool IsValidUserDto(object obj)
        {
            var validationContext = new DataAnotations.ValidationContext(obj);
            var validationResult = new List<DataAnotations.ValidationResult>();

            return DataAnotations.Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }
    }
}

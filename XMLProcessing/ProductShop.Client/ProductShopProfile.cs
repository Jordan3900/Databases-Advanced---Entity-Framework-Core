using AutoMapper;
using ProductShop.Client.Dtos;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.Client
{
    class ProductShopProfile : Profile
    {
        public ProductShopProfile() 
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CategoriesDto, Category>().ReverseMap();
        }
    }
}

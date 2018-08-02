namespace ProductShop.App
{
    using AutoMapper;
    using ProductShop.App.Dto;
    using ProductShop.Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}

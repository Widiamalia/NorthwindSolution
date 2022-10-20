using AutoMapper;
using Northwind.Contracts.Dto.Authentication;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Order;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Product;
using Northwind.Contracts.Dto.Shipper;
using Northwind.Contracts.Dto.Supplier;
using Northwind.Domain.Models;

namespace Northwind.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryForCreateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductForCreateDto>().ReverseMap();
            CreateMap<Product, ProductPhotoGroupDto>().ReverseMap();

            CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoCreateDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoGroupDto>().ReverseMap();

            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, SupplierForCreateDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderForCreateDto>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailForCreateDto>().ReverseMap();

            CreateMap<Shipper, ShipperDto>().ReverseMap();

            CreateMap<UserRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<UserLoginDto, User>().ReverseMap();

        }
    }
}

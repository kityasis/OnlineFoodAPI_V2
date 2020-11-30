using AutoMapper;
using OnlineFood.Data;

namespace OnlineFood.API.ViewModels
{
    public class OnlineFoodMappingProfile : Profile
    {
        public OnlineFoodMappingProfile()
        {

            CreateMap<Order, OrderViewModel>()
              .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
               .ForMember(o => o.OrderDate, ex => ex.MapFrom(o => o.Date))
                .ForMember(o => o.OrderNumber, ex => ex.MapFrom(o => o.Number))
              .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(c => c.Quantity, cm => cm.MapFrom(o => o.Quantity))
                .ForMember(c => c.ProductName, cm => cm.MapFrom(o => o.Product.Name))
                .ForMember(c => c.ProductMRP, cm => cm.MapFrom(o => o.Product.Mrp))
                .ForMember(c => c.UnitPrice, cm => cm.MapFrom(o => o.Product.Price))
                .ForMember(c => c.ProductImageUrl, cm => cm.MapFrom(o => o.Product.ImageUrl));
            CreateMap<Category, DDLViewModel>()
             .ForMember(c => c.Name, cm => cm.MapFrom(o => o.Name));

            CreateMap<SubCategory, DDLViewModel>()
           .ForMember(c => c.Name, cm => cm.MapFrom(o => o.Name));

            CreateMap<Product, ProductViewModel>()
          .ForMember(c => c.categoryId, cm => cm.MapFrom(o => o.SubCategory.CategoryId));

        }
    }
}

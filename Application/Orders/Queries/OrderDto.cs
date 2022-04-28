using Application.Common;
using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Queries
{
    public class OrderDto : IMapFrom<Order>
    {
        public string OrderId { get; set; }
        public int ClientId { get; set; }
        public ICollection<Book> Books { get; set; }
        public decimal TotalPrice { get; set; }
        public string Country { get;  set; }
        public string State { get;  set; }
        public string City { get;  set; }
        public string Street { get;  set; }
        public string Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>()
                .ForMember(m => m.Date, opt => opt.MapFrom(el => el.Date.ToString(Settings.GetDateFormat(), Settings.GetDateProvider())))
                .ForMember(m => m.Country, opt => opt.MapFrom(el => el.Address.Country))
                .ForMember(m => m.State, opt => opt.MapFrom(el => el.Address.State))
                .ForMember(m => m.City, opt => opt.MapFrom(el => el.Address.City))
                .ForMember(m => m.Street, opt => opt.MapFrom(el => el.Address.Street));
        }
    }
}

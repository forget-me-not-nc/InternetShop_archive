using Application.Common;
using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logs.Queries
{
    public class LogDto : IMapFrom<Log>
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Log, LogDto>()
                .ForMember(m => m.Id, opt => opt.MapFrom(p => p.LogId))
                .ForMember(m => m.Date, opt => opt.MapFrom(p => p.Date.ToString(Settings.GetDateFormat(), Settings.GetDateProvider())))
                .ForMember(m => m.Type, opt => opt.MapFrom(p => p.Type.ToString()));
        }
    }
}

using AutoMapper;
using Mango.Service.CouponApi.Models;
using Mango.Service.CouponApi.Models.Dto;

namespace Mango.Service.CouponApi
{
    public class MappingConfig 
    {
        public static MapperConfiguration RegisterMaps ()
        {
            var mappingConfig = new MapperConfiguration(config => 
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
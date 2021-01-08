using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Rocky.Application.Mappers.Profiles;

namespace Rocky.Infra.IoC.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new ProductProfile());
                config.AddProfile(new CategoryProfile());
                config.AddProfile(new ApplicationTypeProfile());
                config.AddProfile(new InquiryHeaderProfile());
                config.AddProfile(new InquiryDetailProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuorumCodingChallengeLegislativeData.Application.Services;
using QuorumCodingChallengeLegislativeData.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuorumCodingChallengeLegislativeData.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped(typeof(ILegislatorService), typeof(LegislatorService));
            services.AddScoped(typeof(IValidateFile), typeof(ValidateFileBills));

            return services;
        }
    }
}

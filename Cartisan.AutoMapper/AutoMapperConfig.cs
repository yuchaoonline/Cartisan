using System.Collections.Generic;
using AutoMapper;
using Cartisan.Infrastructure;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.AutoMapper {
    public static class AutoMapperConfig {
        public static void Initialize() {
            IEnumerable<Profile> profiles = ServiceLocator.GetServices<Profile>();
            Mapper.Initialize(config => profiles.ForEach(profile => config.AddProfile(profile)));
            Mapper.AssertConfigurationIsValid();
        }
    }
}
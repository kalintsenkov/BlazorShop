namespace BlazorShop.Web.Server.Infrastructure
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Common.Mapping;

    using static Common.Constants;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var mapFromType = typeof(IMapFrom<>);
            var explicitMapType = typeof(IMapExplicitly);

            var modelRegistrations = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.StartsWith(ProjectName))
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Type = t,
                    MapFrom = this.GetMappingModel(t, mapFromType),
                    ExplicitMap = t.GetInterfaces()
                        .Where(i => i == explicitMapType)
                        .Select(i => (IMapExplicitly)Activator.CreateInstance(t))
                        .FirstOrDefault()
                });

            foreach (var modelRegistration in modelRegistrations)
            {
                if (modelRegistration.MapFrom != null)
                {
                    this.CreateMap(modelRegistration.MapFrom, modelRegistration.Type);
                }

                modelRegistration.ExplicitMap?.RegisterMappings(this);
            }
        }

        private Type GetMappingModel(Type type, Type mappingInterface)
            => type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == mappingInterface)
                ?.GetGenericArguments()
                .First();
    }
}

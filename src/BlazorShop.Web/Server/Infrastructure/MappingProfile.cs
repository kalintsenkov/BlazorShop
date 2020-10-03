namespace BlazorShop.Web.Server.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    using Common.Mapping;
    using Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
            => this.ApplyMappingsFromAssembly(typeof(ApplicationSettings).Assembly);

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(t => t
                    .GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                const string mappingMethodName = "Mapping";

                var methodInfo = type.GetMethod(mappingMethodName)
                                 ?? type.GetInterface("IMapFrom`1")?.GetMethod(mappingMethodName);

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}

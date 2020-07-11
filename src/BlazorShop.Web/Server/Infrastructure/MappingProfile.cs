namespace BlazorShop.Web.Server.Infrastructure
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Models;

    using static Common.Constants;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var types = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.StartsWith(SystemName))
                .SelectMany(a => a.GetExportedTypes())
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

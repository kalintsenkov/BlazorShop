namespace BlazorShop.Services.Mapping
{
    using AutoMapper;

    public interface IMapExplicitly
    {
        void RegisterMappings(IProfileExpression profile);
    }
}
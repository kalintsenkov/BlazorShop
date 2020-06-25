namespace BlazorShop.Shared.Mapping
{
    using AutoMapper;

    public interface IMapExplicitly
    {
        void RegisterMappings(IProfileExpression profile);
    }
}
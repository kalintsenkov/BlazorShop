namespace BlazorShop.Common.Mapping
{
    using AutoMapper;

    public interface IMapExplicitly
    {
        void RegisterMappings(IProfileExpression profile);
    }
}
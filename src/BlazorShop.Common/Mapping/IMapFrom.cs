namespace BlazorShop.Common.Mapping
{
    using AutoMapper;

    public interface IMapFrom<TModel>
        where TModel : class
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(TModel), this.GetType());
    }
}
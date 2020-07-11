namespace BlazorShop.Models
{
    using AutoMapper;

    public interface IMapFrom<T>
        where T : class
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), this.GetType());
    }
}
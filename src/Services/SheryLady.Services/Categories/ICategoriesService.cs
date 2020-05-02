namespace SheryLady.Services.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<int> Create(string name);

        Task<bool> Update(int id, string name);

        Task<bool> Delete(int id);

        Task<TModel> GetById<TModel>(int id);

        Task<IEnumerable<TModel>> GetAll<TModel>();
    }
}
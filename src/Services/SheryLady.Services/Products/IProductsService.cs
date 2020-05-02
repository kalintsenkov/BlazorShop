namespace SheryLady.Services.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<int> Create(
            string name, 
            string description, 
            string image, 
            int quantity, 
            decimal price, 
            int categoryId);

        Task<bool> Update(
            int id,
            string name,
            string description,
            string image,
            int quantity,
            decimal price,
            int categoryId);

        Task<bool> Delete(int id);

        Task<TModel> GetById<TModel>(int id);

        Task<IEnumerable<TModel>> GetAll<TModel>();

        Task<IEnumerable<TModel>> GetAllByCategoryId<TModel>(int categoryId);
    }
}
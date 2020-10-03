namespace BlazorShop.Services.Addresses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models;
    using Models.Addresses;

    public class AddressesService : BaseService<Address>, IAddressesService
    {
        public AddressesService(BlazorShopDbContext data, IMapper mapper)
            : base(data, mapper)
        {
        }

        public async Task<int> CreateAsync(
            AddressesRequestModel model, string userId)
        {
            var address = new Address
            {
                Country = model.Country,
                State = model.State,
                City = model.City,
                Description = model.Description,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber,
                UserId = userId
            };

            await this.Data.AddAsync(address);
            await this.Data.SaveChangesAsync();

            return address.Id;
        }

        public async Task<Result> DeleteAsync(
            int id, string userId)
        {
            var address = await this
                .All()
                .Where(a => a.Id == id && a.UserId == userId)
                .FirstOrDefaultAsync();

            if (address == null)
            {
                return "This user cannot delete this address.";
            }

            this.Data.Remove(address);

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<IEnumerable<AddressesListingResponseModel>> ByUserAsync(
            string userId)
            => await this.Mapper
                .ProjectTo<AddressesListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(a => a.UserId == userId))
                .ToListAsync();
    }
}

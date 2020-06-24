namespace BlazorShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Web.Shared.Addresses;

    public class AddressesService : BaseService<Address>, IAddressesService
    {
        public AddressesService(ApplicationDbContext data, IMapper mapper)
            : base(data, mapper)
        {
        }

        public async Task<int> CreateAsync(
            string country,
            string state,
            string city,
            string description,
            int postalCode,
            string phoneNumber,
            string userId)
        {
            var address = new Address
            {
                Country = country,
                State = state,
                City = city,
                Description = description,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            await this.Data.AddAsync(address);
            await this.Data.SaveChangesAsync();

            return address.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var address = await this.Data.Addresses.FindAsync(id);
            if (address == null)
            {
                return false;
            }

            this.Data.Remove(address);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AddressListingResponseModel>> GetAllByUserIdAsync(string userId)
            => await this.Mapper
                .ProjectTo<AddressListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(u => u.UserId == userId))
                .ToListAsync();
    }
}

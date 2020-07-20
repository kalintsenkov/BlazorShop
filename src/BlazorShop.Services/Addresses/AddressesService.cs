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
        public AddressesService(ApplicationDbContext data, IMapper mapper)
            : base(data, mapper)
        {
        }

        public async Task<int> CreateAsync(
            string country,
            string state,
            string city,
            string description,
            string postalCode,
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

        public async Task<Result> DeleteAsync(int id, string userId)
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

        public async Task<IEnumerable<AddressesListingResponseModel>> ByUserIdAsync(string userId)
            => await this.Mapper
                .ProjectTo<AddressesListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(u => u.UserId == userId))
                .ToListAsync();
    }
}

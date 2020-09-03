namespace BlazorShop.Services.Addresses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Identity;
    using Models;
    using Models.Addresses;
    using Specifications;
    using Specifications.Addresses;

    public class AddressesService : BaseService<Address>, IAddressesService
    {
        private readonly ICurrentUserService currentUser;

        public AddressesService(
            ApplicationDbContext data,
            IMapper mapper,
            ICurrentUserService currentUser)
            : base(data, mapper)
            => this.currentUser = currentUser;

        public async Task<int> CreateAsync(AddressesRequestModel model)
        {
            var address = new Address
            {
                Country = model.Country,
                State = model.State,
                City = model.City,
                Description = model.Description,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber,
                UserId = this.currentUser.UserId
            };

            await this.Data.AddAsync(address);
            await this.Data.SaveChangesAsync();

            return address.Id;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var specification = this.GetAddressByCurrentUserSpecification()
                .And(new AddressByIdSpecification(id));

            var address = await this
                .All()
                .Where(specification)
                .FirstOrDefaultAsync();

            if (address == null)
            {
                return "This user cannot delete this address.";
            }

            this.Data.Remove(address);

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<IEnumerable<AddressesListingResponseModel>> ByCurrentUserAsync()
            => await this.Mapper
                .ProjectTo<AddressesListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(this.GetAddressByCurrentUserSpecification()))
                .ToListAsync();

        private Specification<Address> GetAddressByCurrentUserSpecification()
            => new AddressByUserIdSpecification(this.currentUser.UserId);
    }
}

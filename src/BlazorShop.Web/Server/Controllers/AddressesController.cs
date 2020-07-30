namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services.Addresses;
    using Models.Addresses;

    [Authorize]
    public class AddressesController : ApiController
    {
        private readonly IAddressesService addressesService;

        public AddressesController(IAddressesService addressesService)
            => this.addressesService = addressesService;

        [HttpGet]
        public async Task<IEnumerable<AddressesListingResponseModel>> All()
            => await this.addressesService.ByUserIdAsync(this.User.GetId());

        [HttpPost]
        public async Task<ActionResult> Create(AddressesRequestModel model)
        {
            var id = await this.addressesService.CreateAsync(
                model.Country,
                model.State,
                model.City,
                model.Description,
                model.PostalCode,
                model.PhoneNumber,
                this.User.GetId());

            return Created(nameof(this.Create), id);
        }

        [HttpDelete(Id)]
        public async Task<ActionResult> Delete(int id)
            => await this.addressesService
                .DeleteAsync(id, this.User.GetId())
                .ToActionResult();
    }
}

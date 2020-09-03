namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Addresses;
    using Services.Addresses;

    [Authorize]
    public class AddressesController : ApiController
    {
        private readonly IAddressesService addresses;

        public AddressesController(IAddressesService addresses)
            => this.addresses = addresses;

        [HttpGet]
        public async Task<IEnumerable<AddressesListingResponseModel>> ByUser()
            => await this.addresses.ByCurrentUserAsync();

        [HttpPost]
        public async Task<ActionResult> Create(
            AddressesRequestModel model)
        {
            var id = await this.addresses.CreateAsync(model);

            return Created(nameof(this.Create), id);
        }

        [HttpDelete(Id)]
        public async Task<ActionResult> Delete(
            int id)
            => await this.addresses
                .DeleteAsync(id)
                .ToActionResult();
    }
}

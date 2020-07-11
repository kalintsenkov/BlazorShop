namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services.Addresses;
    using Shared.Models.Addresses;

    [Authorize]
    public class AddressesController : ApiController
    {
        private readonly IAddressesService addressesService;

        public AddressesController(IAddressesService addressesService)
            => this.addressesService = addressesService;

        [HttpGet]
        public async Task<IEnumerable<AddressListingResponseModel>> All()
            => await this.addressesService.GetAllByUserIdAsync(this.User.GetId());

        [HttpPost]
        public async Task<ActionResult> Create(AddressRequestModel model)
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
        {
            var deleted = await this.addressesService.DeleteAsync(id);
            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

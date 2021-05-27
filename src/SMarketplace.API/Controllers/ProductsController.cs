
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMarketplace.API.Configuration;
using SMarketplace.Application.Commands;
using SMarketplace.Core.Mediator;
using SMarketplace.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SMarketplace.API.Controllers
{
    
    [Route("api/[controller]")]
    public class ProductsController : MainController
    {
        private readonly IProductService _productService;
        private readonly IMediatorHandler _mediator;

        public ProductsController(IProductService productService, IMediatorHandler mediator)
        {
            _productService = productService;
            _mediator = mediator;
        }

        [HttpPost]
        [ClaimsAuthorize("User", "write")]
        public async Task<ActionResult> Register(RegisterProductCommand registerCommand)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _mediator.SendComando(registerCommand));

        }

        [HttpPost("upload")]
        public async Task<ActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if(file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", "").Trim());

                    using(var stream = new FileStream(fullPath,FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest("Erro ao realizar upload");

        }

        [HttpPut]
        [ClaimsAuthorize("User", "write")]
        public async Task<ActionResult> Update(UpdateProductCommand command)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _mediator.SendComando(command));


        }

        [HttpGet]
        [Route("{id}")]
        [ClaimsAuthorize("User", "read")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _productService.Get(id));
        }


        [HttpGet]
        [ClaimsAuthorize("User", "read")]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return Ok(await _productService.GetAll());
        }

        [HttpDelete]
        [Route("{id}")]
        [ClaimsAuthorize("User", "write")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

           return CustomResponse(await _productService.Delete(id));
        }


    }
}

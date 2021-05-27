using Microsoft.AspNetCore.Mvc;
using SMarketplace.Domain.Interfaces;
using SMarketplace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMarketplace.API.Controllers
{
    [Route("api/login")]
    public class AuthController : MainController
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _identityService.LoginAsync(usuarioLogin);

            if (ResponsePossuiErros(result.ResponseResult))
            {
                return CustomResponse();
            }


            if (result.Authenticated)
            {
                return  CustomResponse(result);
            }

            return  Unauthorized("Username or Password is incorrect");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SMarketplace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMarketplace.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        [HttpPost("authentication")]
        public async Task<ActionResult> Login(UserLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _identityService.LoginAsync(usuarioLogin);

            if (ResponsePossuiErros(result.ResponseResult))
            {
                return CustomResponse(result.ResponseResult);
            }


            if (result.Authenticated)
            {


                var response = new
                {
                    Token = result.AccessToken,
                    ExpirationDate = result.ExpiresIn
                };

                return  CustomResponse(response);
            }

            return  Unauthorized("Username or Password is incorrect");
        }
    }
}

using SMarketplace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMarketplace.Domain.Interfaces
{
    public interface IIdentityService
    {

        Task<UserResponseLogin> LoginAsync(UserLogin userLogin);

    }
}

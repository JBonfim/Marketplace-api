using SMarketplace.Core.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMarketplace.Domain.Models
{
    public class UserResponseLogin
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
        public bool Authenticated { get; set; }
        public string Erro { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class UserLoginAuthenticated
    {

        public bool Success { get; set; }
        public string Error { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}

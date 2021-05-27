using System;

namespace SMarketplace.Domain.Models
{
    public class UserLogado
    {
        
        public string Username { get; set; }

        public Guid Id { get; private set; }
        public UserLogado()
        {
            Id = Guid.NewGuid();
        }
    }
}

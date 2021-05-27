using SMarketplace.Core.DomainObjects;
using SMarketplace.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMarketplace.Core.Mediator
{
   public  interface IMediatorHandler
    {
        Task PublicEvento<T>(T evento) where T : Event;
        Task<ResponseMessage> SendComando<T>(T comando) where T : Command;
    }
}

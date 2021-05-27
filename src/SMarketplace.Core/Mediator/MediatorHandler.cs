using MediatR;
using SMarketplace.Core.DomainObjects;
using SMarketplace.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMarketplace.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublicEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        public async Task<ResponseMessage> SendComando<T>(T comando) where T : Command
        {
            return new ResponseMessage(await _mediator.Send(comando), comando);
            
        }
    }
}

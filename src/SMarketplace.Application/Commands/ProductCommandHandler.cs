using FluentValidation.Results;
using MediatR;
using SMarketplace.Core.Data;
using SMarketplace.Core.Messages;
using SMarketplace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMarketplace.Application.Commands
{
    public class ProductCommandHandler : CommandHandler, IRequestHandler<RegisterProductCommand, ValidationResult>
        , IRequestHandler<UpdateProductCommand, ValidationResult>
    {
        private IRepository<Product> _repository;

        public ProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(RegisterProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var entityApi = new Product(message.Name, message.Price, message.Image);
            _repository.AddEntity(entityApi);


            return await PersistirDados(_repository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var product = await _repository.SelectAsync(message.Id);
            if(product == null)
            {
                AdicionarErro("Produto não existe");
            }

            product.AlterProd(message.Name, message.Price, message.Image);
             _repository.UpdateEntity(product);


            return await PersistirDados(_repository.UnitOfWork);
        }
    }
}

using FluentValidation;
using SMarketplace.Core.Messages;
using System;

namespace SMarketplace.Application.Commands
{
    public class UpdateProductCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public UpdateProductCommand()
        {
            AggregateId = Guid.NewGuid();
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarApiValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarApiValidation : AbstractValidator<UpdateProductCommand>
        {
            public RegistrarApiValidation()
            {
                RuleFor(c => c.Price)
                     .NotEmpty()
                    .WithMessage("Preço do Produto não Informado");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do Produto não foi informado");
                RuleFor(c => c.Image)
                    .NotEmpty()
                   .WithMessage("Imagem não Informada");
                RuleFor(c => c.Id)
                   .NotEqual(Guid.Empty)
                  .WithMessage("Id do Produto inválido");
            }


        }
    }

    
}

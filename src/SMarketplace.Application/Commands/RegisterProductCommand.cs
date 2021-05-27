using FluentValidation;
using SMarketplace.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMarketplace.Application.Commands
{
    public class RegisterProductCommand : Command
    {
        public string Name { get;  set; }
        public double Price { get;  set; }
        public string Image { get;  set; }


        public RegisterProductCommand()
        {
            AggregateId = Guid.NewGuid();
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarApiValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarApiValidation : AbstractValidator<RegisterProductCommand>
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
            }


        }
    }

    
}

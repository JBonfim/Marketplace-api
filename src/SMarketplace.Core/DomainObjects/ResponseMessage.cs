using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMarketplace.Core.DomainObjects
{
    public class ResponseMessage
    {
        public ValidationResult ValidationResult { get; set; }
        public Object Model { get; set; }

        public ResponseMessage(ValidationResult validationResult, Object model = null)
        {
            ValidationResult = validationResult;
            Model = model;
        }


    }
}

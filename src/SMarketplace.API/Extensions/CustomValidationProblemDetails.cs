using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMarketplace.API.Extensions
{
    public class CustomValidationProblemDetails
    {
        public string TraceId { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public int status { get; set; }
        public string title { get; set; }

        public CustomValidationProblemDetails()
        {

        }
        public CustomValidationProblemDetails(IDictionary<string, string[]> errors)
        {
            Errors = errors;
        }

        public CustomValidationProblemDetails(ModelStateDictionary modelState)
        {

        }
    }
}

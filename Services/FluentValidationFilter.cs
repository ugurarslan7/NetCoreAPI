using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(p => p.Errors)
                 .Select(p => p.ErrorMessage).ToList();

                var resultModel = ServiceResult.Fail(errors);
                context.Result = new BadRequestObjectResult(resultModel);
                return;
            }

            await next();
        }
    }
}

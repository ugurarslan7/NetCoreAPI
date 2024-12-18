using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Filters
{
    public class NotFoundFilter<T, TId>(IGenericRepository<T, TId> genericRepository) : Attribute, IAsyncActionFilter where T : class
        where TId : struct
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue is not TId id)
            {
                await next();
                return;
            }

            if (await genericRepository.AnyAsync(id))
            {
                await next();
                return;
            }

            var enetiyName = typeof(T).Name;
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var result = ServiceResult.Fail($"Entity Not Found.({enetiyName})({actionName})");
            context.Result = new NotFoundObjectResult(result);
        }
    }
}
